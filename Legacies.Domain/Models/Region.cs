using Legacies.Domain.Constants;
using Legacies.Domain.Enums;

namespace Legacies.Domain.Models
{
    public sealed class Region
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int BaseEcologicalSupport { get; set; }

        public decimal BaseEnvironmentalPressure { get; set; }

        public decimal SeasonalVolatility { get; set; }

        public int SeasonalPeakMonth { get; set; } = RegionConstants.DefaultSeasonalPeakMonth;

        public decimal CurrentEnvironmentalPressure { get; set; }

        public int CurrentMonthlySupport { get; set; }

        public RegionSupportBand SupportBand { get; set; } = RegionSupportBand.Stable;
    }
}
