using Legacies.Domain.Constants;
using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class EcologySystem : ISimulationSystem
    {
        public string Name => nameof(EcologySystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Ecology;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
            foreach (Region region in world.Regions.OrderBy(region => region.Id))
            {
                int previousSupport = region.CurrentMonthlySupport;
                RegionSupportBand previousBand = region.SupportBand;

                int calculatedSupport = (int)Math.Round(
                    region.BaseEcologicalSupport * (EcologyConstants.FullSupportRatio - region.CurrentEnvironmentalPressure),
                    MidpointRounding.AwayFromZero);

                region.CurrentMonthlySupport = Math.Max(EcologyConstants.MinimumMonthlySupport, calculatedSupport);
                region.SupportBand = ClassifySupportBand(region.BaseEcologicalSupport, region.CurrentMonthlySupport);

                result.RegionConditionChanges.Add(
                    new RegionConditionChange(
                        region.Id,
                        region.Name,
                        previousSupport,
                        region.CurrentMonthlySupport,
                        previousBand,
                        region.SupportBand,
                        region.CurrentEnvironmentalPressure));
            }
        }

        private static RegionSupportBand ClassifySupportBand(int baseSupport, int currentSupport)
        {
            if (baseSupport <= EcologyConstants.MinimumMonthlySupport)
            {
                return RegionSupportBand.Harsh;
            }

            decimal supportRatio = currentSupport / (decimal)baseSupport;

            if (supportRatio < EcologyConstants.HarshSupportRatioThreshold)
            {
                return RegionSupportBand.Harsh;
            }

            if (supportRatio < EcologyConstants.StrainedSupportRatioThreshold)
            {
                return RegionSupportBand.Strained;
            }

            if (supportRatio > EcologyConstants.AbundantSupportRatioThreshold)
            {
                return RegionSupportBand.Abundant;
            }

            return RegionSupportBand.Stable;
        }
    }
}
