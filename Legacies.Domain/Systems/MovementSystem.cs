using Legacies.Domain.Constants;
using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class MovementSystem : ISimulationSystem
    {
        public string Name => nameof(MovementSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Movement;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
            Dictionary<int, Region> regionsById = world.Regions.ToDictionary(region => region.Id);
            Dictionary<int, Species> speciesById = world.Species.ToDictionary(species => species.Id);
            Dictionary<int, int> regionalPopulationById = world.PopulationGroups
                .GroupBy(population => population.RegionId)
                .ToDictionary(group => group.Key, group => group.Sum(population => population.Size));

            foreach (PopulationGroup population in world.PopulationGroups.OrderBy(population => population.Id))
            {
                if (population.Size <= PopulationConstants.MinimumPopulationSize)
                {
                    continue;
                }

                if (!regionsById.TryGetValue(population.RegionId, out Region? currentRegion) || !speciesById.TryGetValue(population.SpeciesId, out Species? species))
                {
                    continue;
                }

                population.HomeRegionId ??= population.RegionId;

                int currentRegionalPopulation = regionalPopulationById.GetValueOrDefault(currentRegion.Id);
                decimal currentSupportRatio = CalculateSupportRatio(currentRegion.CurrentMonthlySupport, currentRegionalPopulation);

                population.MovementPressure = UpdatePressure(
                    population.MovementPressure,
                    CalculateMovementPressureTarget(population, currentRegion));

                population.DisplacementPressure = UpdatePressure(
                    population.DisplacementPressure,
                    CalculateDisplacementPressureTarget(population, currentRegion));

                DestinationEvaluation? bestDestination = EvaluateBestDestination(
                    population,
                    currentRegion,
                    currentSupportRatio,
                    regionalPopulationById,
                    regionsById);

                population.OpportunityPressure = UpdatePressure(
                    population.OpportunityPressure,
                    bestDestination?.OpportunityScore ?? MovementConstants.MinimumPressure,
                    MovementConstants.OpportunityPressureDecay);

                if (bestDestination is null || !ShouldMove(population, currentRegion, bestDestination, world.CurrentDate.AbsoluteMonth))
                {
                    continue;
                }

                regionalPopulationById[currentRegion.Id] = currentRegionalPopulation - population.Size;
                regionalPopulationById[bestDestination.Region.Id] = regionalPopulationById.GetValueOrDefault(bestDestination.Region.Id) + population.Size;

                population.RegionId = bestDestination.Region.Id;
                population.LastMoveAbsoluteMonth = world.CurrentDate.AbsoluteMonth;

                result.MovementChanges.Add(
                    new MovementChangeSummary(
                        population.Id,
                        species.Id,
                        species.Name,
                        currentRegion.Id,
                        currentRegion.Name,
                        bestDestination.Region.Id,
                        bestDestination.Region.Name,
                        population.Size,
                        currentRegion.SupportBand,
                        bestDestination.Region.SupportBand,
                        population.MovementPressure,
                        population.DisplacementPressure,
                        bestDestination.IsForced,
                        population.HomeRegionId == bestDestination.Region.Id));
            }
        }

        private static decimal CalculateSupportRatio(int monthlySupport, int populationSize)
        {
            if (populationSize <= PopulationConstants.MinimumPopulationSize)
            {
                return PopulationConstants.DefaultHealth;
            }

            return monthlySupport / (decimal)populationSize;
        }

        private static decimal CalculateMovementPressureTarget(PopulationGroup population, Region currentRegion)
        {
            decimal healthStrain = Math.Max(MovementConstants.MinimumPressure, PopulationConstants.DefaultHealth - population.Health);
            decimal target = (population.SupportPressure * MovementConstants.SupportPressureWeight)
                + (healthStrain * MovementConstants.HealthStrainWeight)
                + (currentRegion.SupportBand switch
                {
                    RegionSupportBand.Harsh => MovementConstants.HarshRegionMovementPressure,
                    RegionSupportBand.Strained => MovementConstants.StrainedRegionMovementPressure,
                    _ => MovementConstants.MinimumPressure
                });

            return Math.Clamp(target, MovementConstants.MinimumPressure, MovementConstants.MaximumPressure);
        }

        private static decimal CalculateDisplacementPressureTarget(PopulationGroup population, Region currentRegion)
        {
            decimal target = currentRegion.SupportBand switch
            {
                RegionSupportBand.Harsh => MovementConstants.HarshRegionDisplacementPressure,
                RegionSupportBand.Strained => MovementConstants.StrainedRegionDisplacementPressure,
                _ => MovementConstants.MinimumPressure
            };

            target += Math.Max(MovementConstants.MinimumPressure, population.SupportPressure - MovementConstants.DisplacementSupportPressureFloor) * MovementConstants.DisplacementSupportPressureWeight;

            if (population.HomeRegionId.HasValue && population.HomeRegionId.Value != currentRegion.Id)
            {
                target += MovementConstants.AwayFromHomeDisplacementPressure;
            }

            return Math.Clamp(target, MovementConstants.MinimumPressure, MovementConstants.MaximumPressure);
        }

        private static DestinationEvaluation? EvaluateBestDestination(
            PopulationGroup population,
            Region currentRegion,
            decimal currentSupportRatio,
            Dictionary<int, int> regionalPopulationById,
            Dictionary<int, Region> regionsById)
        {
            DestinationEvaluation? bestDestination = null;
            bool isForced = population.DisplacementPressure >= MovementConstants.ForcedDisplacementPressureThreshold;

            foreach (int adjacentRegionId in currentRegion.AdjacentRegionIds.OrderBy(regionId => regionId))
            {
                if (adjacentRegionId == currentRegion.Id || !regionsById.TryGetValue(adjacentRegionId, out Region? destinationRegion))
                {
                    continue;
                }

                int projectedDestinationPopulation = regionalPopulationById.GetValueOrDefault(destinationRegion.Id) + population.Size;
                decimal projectedSupportRatio = CalculateSupportRatio(destinationRegion.CurrentMonthlySupport, projectedDestinationPopulation);

                if (projectedSupportRatio < MovementConstants.MinimumProjectedDestinationSupportRatio)
                {
                    continue;
                }

                decimal opportunityScore = CalculateOpportunityScore(population, currentRegion, destinationRegion, currentSupportRatio, projectedSupportRatio);

                if (opportunityScore < MovementConstants.MinimumOpportunityScore)
                {
                    continue;
                }

                DestinationEvaluation evaluation = new(destinationRegion, opportunityScore, isForced);

                if (bestDestination is null
                    || evaluation.OpportunityScore > bestDestination.OpportunityScore
                    || (evaluation.OpportunityScore == bestDestination.OpportunityScore && evaluation.Region.Id < bestDestination.Region.Id))
                {
                    bestDestination = evaluation;
                }
            }

            return bestDestination;
        }

        private static decimal CalculateOpportunityScore(
            PopulationGroup population,
            Region currentRegion,
            Region destinationRegion,
            decimal currentSupportRatio,
            decimal projectedSupportRatio)
        {
            decimal score = ((projectedSupportRatio - currentSupportRatio) * MovementConstants.SupportRatioOpportunityWeight)
                + ((currentRegion.CurrentEnvironmentalPressure - destinationRegion.CurrentEnvironmentalPressure) * MovementConstants.EnvironmentalPressureOpportunityWeight)
                - MovementConstants.MoveFriction;

            score += destinationRegion.SupportBand switch
            {
                RegionSupportBand.Abundant => MovementConstants.AbundantDestinationOpportunityBonus,
                RegionSupportBand.Stable => MovementConstants.StableDestinationOpportunityBonus,
                RegionSupportBand.Strained => -MovementConstants.StrainedDestinationOpportunityPenalty,
                RegionSupportBand.Harsh => -MovementConstants.HarshDestinationOpportunityPenalty,
                _ => MovementConstants.MinimumPressure
            };

            if (population.HomeRegionId == destinationRegion.Id)
            {
                score += MovementConstants.ReturnHomeOpportunityBonus;
            }

            return score;
        }

        private static decimal UpdatePressure(decimal currentValue, decimal targetValue, decimal decayFactor = MovementConstants.DefaultPressureDecay)
        {
            decimal updatedValue = targetValue + (currentValue * decayFactor);
            return Math.Clamp(updatedValue, MovementConstants.MinimumPressure, MovementConstants.MaximumPressure);
        }

        private static bool ShouldMove(PopulationGroup population, Region currentRegion, DestinationEvaluation destination, int currentAbsoluteMonth)
        {
            decimal stayingPressure = MovementConstants.BaseStayingPressure;

            if (population.HomeRegionId == currentRegion.Id)
            {
                stayingPressure += MovementConstants.HomeRegionResistance;
            }

            if (population.LastMoveAbsoluteMonth.HasValue
                && currentAbsoluteMonth - population.LastMoveAbsoluteMonth.Value < MovementConstants.RecentMoveCooldownMonths)
            {
                stayingPressure += MovementConstants.RecentMoveResistance;
            }

            if (population.HomeRegionId == destination.Region.Id)
            {
                stayingPressure = Math.Max(MovementConstants.MinimumPressure, stayingPressure - MovementConstants.ReturnHomeResistanceRelief);
            }

            decimal decisionPressure = population.MovementPressure + population.DisplacementPressure + population.OpportunityPressure;

            return decisionPressure >= stayingPressure + MovementConstants.MovementDecisionThreshold;
        }

        private sealed record DestinationEvaluation(Region Region, decimal OpportunityScore, bool IsForced);
    }
}
