namespace Legacies.Domain.Constants
{
    public static class MovementConstants
    {
        public const decimal MinimumPressure = 0m;
        public const decimal MaximumPressure = 1.5m;
        public const decimal DefaultPressureDecay = 0.55m;
        public const decimal OpportunityPressureDecay = 0.35m;
        public const decimal SupportPressureWeight = 0.90m;
        public const decimal HealthStrainWeight = 0.60m;
        public const decimal StrainedRegionMovementPressure = 0.08m;
        public const decimal HarshRegionMovementPressure = 0.24m;
        public const decimal StrainedRegionDisplacementPressure = 0.12m;
        public const decimal HarshRegionDisplacementPressure = 0.32m;
        public const decimal DisplacementSupportPressureFloor = 0.10m;
        public const decimal DisplacementSupportPressureWeight = 0.75m;
        public const decimal AwayFromHomeDisplacementPressure = 0.06m;
        public const decimal SupportRatioOpportunityWeight = 0.80m;
        public const decimal EnvironmentalPressureOpportunityWeight = 0.35m;
        public const decimal StableDestinationOpportunityBonus = 0.08m;
        public const decimal AbundantDestinationOpportunityBonus = 0.16m;
        public const decimal StrainedDestinationOpportunityPenalty = 0.08m;
        public const decimal HarshDestinationOpportunityPenalty = 0.24m;
        public const decimal ReturnHomeOpportunityBonus = 0.06m;
        public const decimal MoveFriction = 0.12m;
        public const decimal MinimumProjectedDestinationSupportRatio = 0.60m;
        public const decimal MinimumOpportunityScore = 0.10m;
        public const decimal BaseStayingPressure = 0.45m;
        public const decimal HomeRegionResistance = 0.18m;
        public const decimal RecentMoveResistance = 0.24m;
        public const decimal ReturnHomeResistanceRelief = 0.10m;
        public const decimal MovementDecisionThreshold = 0.14m;
        public const decimal ForcedDisplacementPressureThreshold = 0.60m;
        public const int RecentMoveCooldownMonths = 6;
    }
}
