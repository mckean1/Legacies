namespace Legacies.Domain.Constants
{
    public static class EnvironmentConstants
    {
        public const decimal MinimumEnvironmentalPressure = -0.25m;
        public const decimal MaximumEnvironmentalPressure = 0.95m;
        public const decimal HalfYearInMonths = WorldConstants.MonthsInYear / 2m;
        public const decimal SeasonalNeutralDistance = 0.5m;
    }
}
