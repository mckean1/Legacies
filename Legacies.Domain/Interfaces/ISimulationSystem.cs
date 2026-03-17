using Legacies.Domain.Enums;
using Legacies.Domain.Models;

namespace Legacies.Domain.Interfaces
{
    public interface ISimulationSystem
    {
        string Name { get; }

        SimulationSystemPhase Phase { get; }

        void Execute(World world, SimulationContext context, SimulationStepResult result);
    }
}
