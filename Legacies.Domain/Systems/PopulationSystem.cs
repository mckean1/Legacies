using Legacies.Domain.Constants;
using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class PopulationSystem : ISimulationSystem
    {
        public string Name => nameof(PopulationSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Population;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
            foreach (Region region in world.Regions.OrderBy(region => region.Id))
            {
                List<PopulationGroup> regionPopulations = world.PopulationGroups
                    .Where(population => population.RegionId == region.Id)
                    .OrderBy(population => population.Id)
                    .ToList();

                if (regionPopulations.Count == 0)
                {
                    continue;
                }

                int regionalPopulation = regionPopulations.Sum(population => population.Size);
                decimal supportRatio = regionalPopulation == PopulationConstants.MinimumPopulationSize
                    ? PopulationConstants.MinimumSupportRatio
                    : region.CurrentMonthlySupport / (decimal)regionalPopulation;

                foreach (PopulationGroup population in regionPopulations)
                {
                    Species species = world.GetSpecies(population.SpeciesId);
                    int previousSize = population.Size;
                    decimal previousSupportPressure = population.SupportPressure;

                    int populationDelta = CalculatePopulationDelta(population.Size, supportRatio, species);
                    population.Size = Math.Max(PopulationConstants.MinimumPopulationSize, population.Size + populationDelta);
                    population.SupportPressure = Math.Clamp(PopulationConstants.DefaultHealth - supportRatio, PopulationConstants.MinimumSupportPressure, PopulationConstants.MaximumSupportPressure);
                    population.Health = CalculateHealth(population.Health, supportRatio, population.Size == PopulationConstants.MinimumPopulationSize);

                    result.PopulationChanges.Add(
                        new PopulationChangeSummary(
                            population.Id,
                            species.Id,
                            species.Name,
                            region.Id,
                            region.Name,
                            previousSize,
                            population.Size,
                            previousSupportPressure,
                            population.SupportPressure));
                }
            }
        }

        private static int CalculatePopulationDelta(int currentSize, decimal supportRatio, Species species)
        {
            if (currentSize <= PopulationConstants.MinimumPopulationSize)
            {
                return PopulationConstants.NoPopulationChange;
            }

            if (supportRatio >= PopulationConstants.MinimumSupportRatioForGrowth)
            {
                decimal growthRate = Math.Min(species.MonthlyGrowthRate + ((supportRatio - PopulationConstants.DefaultHealth) * PopulationConstants.GrowthBonusPerSupportPoint), PopulationConstants.MaximumGrowthRate);
                return Math.Max(PopulationConstants.MinimumPopulationIncrease, (int)Math.Round(currentSize * growthRate, MidpointRounding.AwayFromZero));
            }

            if (supportRatio >= PopulationConstants.MinimumSupportRatioForStability)
            {
                return PopulationConstants.NoPopulationChange;
            }

            decimal declineRate = Math.Min(((PopulationConstants.DefaultHealth - supportRatio) * species.ScarcityDeclineRate) + PopulationConstants.BaseDeclineRatePenalty, PopulationConstants.MaximumDeclineRate);
            return -Math.Max(PopulationConstants.MinimumPopulationDecrease, (int)Math.Round(currentSize * declineRate, MidpointRounding.AwayFromZero));
        }

        private static decimal CalculateHealth(decimal currentHealth, decimal supportRatio, bool populationCollapsed)
        {
            if (populationCollapsed)
            {
                return PopulationConstants.MinimumHealth;
            }

            decimal healthDelta = supportRatio >= PopulationConstants.DefaultHealth
                ? PopulationConstants.HealthRecoveryPerMonth
                : -Math.Min(PopulationConstants.MaximumHealthDecline, ((PopulationConstants.DefaultHealth - supportRatio) * PopulationConstants.HealthDeclinePerSupportGap) + PopulationConstants.BaseHealthDecline);

            return Math.Clamp(currentHealth + healthDelta, PopulationConstants.MinimumHealth, PopulationConstants.MaximumHealth);
        }
    }
}
