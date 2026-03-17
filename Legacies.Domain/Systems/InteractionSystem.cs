using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class InteractionSystem : ISimulationSystem
    {
        public string Name => nameof(InteractionSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Interaction;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
        }
    }
}
