namespace Legacies.Domain.Models
{
    public sealed class SimulationRunOptions
    {
        public static SimulationRunOptions Default { get; } = new();

        public int? Seed { get; init; }
    }
}
