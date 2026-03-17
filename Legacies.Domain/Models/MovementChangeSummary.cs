using Legacies.Domain.Enums;

namespace Legacies.Domain.Models
{
    public sealed record MovementChangeSummary(
        int PopulationGroupId,
        int SpeciesId,
        string SpeciesName,
        int OriginRegionId,
        string OriginRegionName,
        int DestinationRegionId,
        string DestinationRegionName,
        int PopulationSize,
        RegionSupportBand OriginSupportBand,
        RegionSupportBand DestinationSupportBand,
        decimal MovementPressure,
        decimal DisplacementPressure,
        bool WasForced,
        bool ReturnedHome);
}
