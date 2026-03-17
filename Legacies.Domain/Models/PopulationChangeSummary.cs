namespace Legacies.Domain.Models
{
    public sealed record PopulationChangeSummary(
        int PopulationGroupId,
        int SpeciesId,
        string SpeciesName,
        int RegionId,
        string RegionName,
        int PreviousSize,
        int CurrentSize,
        decimal PreviousSupportPressure,
        decimal CurrentSupportPressure);
}
