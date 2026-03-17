namespace Legacies.Domain.Constants
{
    public static class PopulationConstants
    {
        public const int MinimumPopulationSize = 0;
        public const int NoPopulationChange = 0;
        public const int MinimumPopulationIncrease = 1;
        public const int MinimumPopulationDecrease = 1;
        public const decimal MinimumSupportRatio = 0m;
        public const decimal MinimumHealth = 0m;
        public const decimal DefaultHealth = 1m;
        public const decimal MaximumHealth = 1m;
        public const decimal MinimumSupportPressure = 0m;
        public const decimal MaximumSupportPressure = 1.5m;
        public const decimal MinimumSupportRatioForGrowth = 1.05m;
        public const decimal MinimumSupportRatioForStability = 0.95m;
        public const decimal GrowthBonusPerSupportPoint = 0.15m;
        public const decimal MaximumGrowthRate = 0.08m;
        public const decimal BaseDeclineRatePenalty = 0.01m;
        public const decimal MaximumDeclineRate = 0.35m;
        public const decimal HealthRecoveryPerMonth = 0.03m;
        public const decimal MaximumHealthDecline = 0.20m;
        public const decimal HealthDeclinePerSupportGap = 0.25m;
        public const decimal BaseHealthDecline = 0.02m;
    }
}
