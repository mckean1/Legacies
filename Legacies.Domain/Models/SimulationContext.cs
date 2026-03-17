namespace Legacies.Domain.Models
{
    public sealed class SimulationContext
    {
        public SimulationContext(int tickIndex, SimulationRunOptions options)
        {
            TickIndex = tickIndex;
            Options = options;
        }

        public int TickIndex { get; }

        public SimulationRunOptions Options { get; }
    }
}
