using Legacies.Domain.Constants;
using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class EnvironmentSystem : ISimulationSystem
    {
        public string Name => nameof(EnvironmentSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Environment;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
            foreach (Region region in world.Regions.OrderBy(region => region.Id))
            {
                decimal seasonalOffset = CalculateSeasonalOffset(world.CurrentDate.Month, region.SeasonalPeakMonth, region.SeasonalVolatility);
                region.CurrentEnvironmentalPressure = Math.Clamp(region.BaseEnvironmentalPressure + seasonalOffset, EnvironmentConstants.MinimumEnvironmentalPressure, EnvironmentConstants.MaximumEnvironmentalPressure);
            }
        }

        private static decimal CalculateSeasonalOffset(int month, int peakMonth, decimal seasonalVolatility)
        {
            int monthDistance = Math.Abs(month - peakMonth);
            monthDistance = Math.Min(monthDistance, WorldConstants.MonthsInYear - monthDistance);

            decimal normalizedDistance = monthDistance / EnvironmentConstants.HalfYearInMonths;
            return (normalizedDistance - EnvironmentConstants.SeasonalNeutralDistance) * seasonalVolatility;
        }
    }
}
