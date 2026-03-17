using Legacies.Domain.Models;

namespace Legacies.Client.Constants
{
    public static class SampleWorldConstants
    {
        public static IReadOnlyList<Region> CreateRegions() =>
        [
            new Region
            {
                Id = 1,
                Name = "Green Basin",
                AdjacentRegionIds = [2],
                BaseEcologicalSupport = 220,
                BaseEnvironmentalPressure = 0.12m,
                SeasonalVolatility = 0.14m,
                SeasonalPeakMonth = 5
            },
            new Region
            {
                Id = 2,
                Name = "High Steppe",
                AdjacentRegionIds = [1, 3],
                BaseEcologicalSupport = 130,
                BaseEnvironmentalPressure = 0.18m,
                SeasonalVolatility = 0.16m,
                SeasonalPeakMonth = 7
            },
            new Region
            {
                Id = 3,
                Name = "River Plain",
                AdjacentRegionIds = [2],
                BaseEcologicalSupport = 340,
                BaseEnvironmentalPressure = 0.10m,
                SeasonalVolatility = 0.08m,
                SeasonalPeakMonth = 4
            }
        ];

        public static IReadOnlyList<Species> CreateSpecies() =>
        [
            new Species
            {
                Id = 1,
                Name = "Foragers",
                MonthlyGrowthRate = 0.03m,
                ScarcityDeclineRate = 0.80m
            },
            new Species
            {
                Id = 2,
                Name = "Herds",
                MonthlyGrowthRate = 0.04m,
                ScarcityDeclineRate = 0.65m
            }
        ];

        public static IReadOnlyList<PopulationGroup> CreatePopulationGroups() =>
        [
            new PopulationGroup
            {
                Id = 1,
                SpeciesId = 1,
                RegionId = 1,
                Size = 170,
                Health = 0.82m
            },
            new PopulationGroup
            {
                Id = 2,
                SpeciesId = 1,
                RegionId = 2,
                Size = 180,
                Health = 0.74m
            },
            new PopulationGroup
            {
                Id = 3,
                SpeciesId = 2,
                RegionId = 3,
                Size = 210,
                Health = 0.86m
            }
        ];
    }
}
