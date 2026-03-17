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
                decimal supportRatio = regionalPopulation == 0
                    ? 0m
                    : region.CurrentMonthlySupport / (decimal)regionalPopulation;

                foreach (PopulationGroup population in regionPopulations)
                {
                    Species species = world.GetSpecies(population.SpeciesId);
                    int previousSize = population.Size;
                    decimal previousSupportPressure = population.SupportPressure;

                    int populationDelta = CalculatePopulationDelta(population.Size, supportRatio, species);
                    population.Size = Math.Max(0, population.Size + populationDelta);
                    population.SupportPressure = Math.Clamp(1m - supportRatio, 0m, 1.5m);
                    population.Health = CalculateHealth(population.Health, supportRatio, population.Size == 0);

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
            if (currentSize <= 0)
            {
                return 0;
            }

            if (supportRatio >= 1.05m)
            {
                decimal growthRate = Math.Min(species.MonthlyGrowthRate + ((supportRatio - 1m) * 0.15m), 0.08m);
                return Math.Max(1, (int)Math.Round(currentSize * growthRate, MidpointRounding.AwayFromZero));
            }

            if (supportRatio >= 0.95m)
            {
                return 0;
            }

            decimal declineRate = Math.Min(((1m - supportRatio) * species.ScarcityDeclineRate) + 0.01m, 0.35m);
            return -Math.Max(1, (int)Math.Round(currentSize * declineRate, MidpointRounding.AwayFromZero));
        }

        private static decimal CalculateHealth(decimal currentHealth, decimal supportRatio, bool populationCollapsed)
        {
            if (populationCollapsed)
            {
                return 0m;
            }

            decimal healthDelta = supportRatio >= 1m
                ? 0.03m
                : -Math.Min(0.20m, ((1m - supportRatio) * 0.25m) + 0.02m);

            return Math.Clamp(currentHealth + healthDelta, 0m, 1m);
        }
    }
}
