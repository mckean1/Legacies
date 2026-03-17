using Legacies.Domain.Enums;

namespace Legacies.Domain.Models
{
    public sealed record RegionConditionChange(
        int RegionId,
        string RegionName,
        int PreviousSupport,
        int CurrentSupport,
        RegionSupportBand PreviousBand,
        RegionSupportBand CurrentBand,
        decimal EnvironmentalPressure);
}
