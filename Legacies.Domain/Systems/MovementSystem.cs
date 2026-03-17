using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class MovementSystem : ISimulationSystem
    {
        public string Name => nameof(MovementSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Movement;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
        }
    }
}
