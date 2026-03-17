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
                    region.BaseEcologicalSupport * (1m - region.CurrentEnvironmentalPressure),
                    MidpointRounding.AwayFromZero);

                region.CurrentMonthlySupport = Math.Max(0, calculatedSupport);
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
            if (baseSupport <= 0)
            {
                return RegionSupportBand.Harsh;
            }

            decimal supportRatio = currentSupport / (decimal)baseSupport;

            if (supportRatio < 0.65m)
            {
                return RegionSupportBand.Harsh;
            }

            if (supportRatio < 0.90m)
            {
                return RegionSupportBand.Strained;
            }

            if (supportRatio > 1.10m)
            {
                return RegionSupportBand.Abundant;
            }

            return RegionSupportBand.Stable;
        }
    }
}
