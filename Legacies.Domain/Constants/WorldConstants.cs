namespace Legacies.Domain.Constants
{
    public static class WorldConstants
    {
        public const int FirstYear = 1;
        public const int FirstMonthOfYear = 1;
        public const int MonthsInYear = 12;
        public const int MonthsPerSeason = 3;
        public const int SummerStartMonth = FirstMonthOfYear + MonthsPerSeason;
        public const int AutumnStartMonth = SummerStartMonth + MonthsPerSeason;
        public const int WinterStartMonth = AutumnStartMonth + MonthsPerSeason;
    }
}
