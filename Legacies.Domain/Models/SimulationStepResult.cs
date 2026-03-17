namespace Legacies.Domain.Models
{
    public sealed class SimulationStepResult
    {
        public SimulationStepResult(int tickIndex, WorldDate startDate)
        {
            TickIndex = tickIndex;
            StartDate = startDate;
            EndDate = startDate;
        }

        public int TickIndex { get; }

        public WorldDate StartDate { get; }

        public WorldDate EndDate { get; internal set; }

        public List<string> ExecutedSystems { get; } = new();

        public List<ChronicleEvent> ChronicleEvents { get; } = new();

        public List<string> Notes { get; } = new();

        public List<RegionConditionChange> RegionConditionChanges { get; } = new();

        public List<PopulationChangeSummary> PopulationChanges { get; } = new();
    }
}
