using Legacies.Domain.Constants;
using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class ChronicleSystem : ISimulationSystem
    {
        public string Name => nameof(ChronicleSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Chronicle;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
            foreach (RegionConditionChange regionChange in result.RegionConditionChanges.OrderBy(change => change.RegionId))
            {
                ChronicleEvent? chronicleEvent = CreateRegionEvent(world.CurrentDate, regionChange);

                if (chronicleEvent is not null)
                {
                    result.ChronicleEvents.Add(chronicleEvent);
                    world.ChronicleHistory.Add(chronicleEvent);
                }
            }

            foreach (PopulationChangeSummary populationChange in result.PopulationChanges.OrderBy(change => change.PopulationGroupId))
            {
                ChronicleEvent? chronicleEvent = CreatePopulationEvent(world.CurrentDate, populationChange);

                if (chronicleEvent is not null)
                {
                    result.ChronicleEvents.Add(chronicleEvent);
                    world.ChronicleHistory.Add(chronicleEvent);
                }
            }
        }

        private static ChronicleEvent? CreateRegionEvent(WorldDate currentDate, RegionConditionChange change)
        {
            if (change.PreviousBand == change.CurrentBand)
            {
                return null;
            }

            return change.CurrentBand switch
            {
                RegionSupportBand.Harsh => new ChronicleEvent(
                    currentDate.Clone(),
                    ChronicleEventCategory.Scarcity,
                    ChronicleEventImportance.High,
                    $"{change.RegionName} entered harsh ecological conditions.")
                {
                    RegionId = change.RegionId
                },
                RegionSupportBand.Strained when change.PreviousBand is RegionSupportBand.Stable or RegionSupportBand.Abundant => new ChronicleEvent(
                    currentDate.Clone(),
                    ChronicleEventCategory.Scarcity,
                    ChronicleEventImportance.Medium,
                    $"Support tightened across {change.RegionName}.")
                {
                    RegionId = change.RegionId
                },
                RegionSupportBand.Stable when change.PreviousBand is RegionSupportBand.Harsh or RegionSupportBand.Strained => new ChronicleEvent(
                    currentDate.Clone(),
                    ChronicleEventCategory.Recovery,
                    ChronicleEventImportance.Medium,
                    $"{change.RegionName} returned to stable support conditions.")
                {
                    RegionId = change.RegionId
                },
                RegionSupportBand.Abundant => new ChronicleEvent(
                    currentDate.Clone(),
                    ChronicleEventCategory.Recovery,
                    ChronicleEventImportance.Low,
                    $"{change.RegionName} enjoyed an abundant month.")
                {
                    RegionId = change.RegionId
                },
                _ => null
            };
        }

        private static ChronicleEvent? CreatePopulationEvent(WorldDate currentDate, PopulationChangeSummary change)
        {
            if (change.PreviousSize <= ChronicleConstants.MinimumPreviousPopulationSize)
            {
                return null;
            }

            decimal changeRatio = (change.CurrentSize - change.PreviousSize) / (decimal)change.PreviousSize;

            if (change.CurrentSize == ChronicleConstants.CollapsedPopulationSize)
            {
                return new ChronicleEvent(
                    currentDate.Clone(),
                    ChronicleEventCategory.PopulationCollapse,
                    ChronicleEventImportance.High,
                    $"The {change.SpeciesName} population in {change.RegionName} collapsed.")
                {
                    PopulationGroupId = change.PopulationGroupId,
                    RegionId = change.RegionId,
                    SpeciesId = change.SpeciesId
                };
            }

            if (change.PreviousSupportPressure < ChronicleConstants.PressureEventThreshold && change.CurrentSupportPressure >= ChronicleConstants.PressureEventThreshold && changeRatio <= ChronicleConstants.DeclineEventChangeRatioThreshold)
            {
                return new ChronicleEvent(
                    currentDate.Clone(),
                    ChronicleEventCategory.PopulationDecline,
                    ChronicleEventImportance.Medium,
                    $"The {change.SpeciesName} population in {change.RegionName} began to decline under pressure.")
                {
                    PopulationGroupId = change.PopulationGroupId,
                    RegionId = change.RegionId,
                    SpeciesId = change.SpeciesId
                };
            }

            if (change.PreviousSupportPressure >= ChronicleConstants.PressureEventThreshold && change.CurrentSupportPressure < ChronicleConstants.RecoveryPressureThreshold && changeRatio >= ChronicleConstants.GrowthEventChangeRatioThreshold)
            {
                return new ChronicleEvent(
                    currentDate.Clone(),
                    ChronicleEventCategory.PopulationGrowth,
                    ChronicleEventImportance.Low,
                    $"The {change.SpeciesName} population in {change.RegionName} recovered and grew.")
                {
                    PopulationGroupId = change.PopulationGroupId,
                    RegionId = change.RegionId,
                    SpeciesId = change.SpeciesId
                };
            }

            return null;
        }
    }
}
