using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class PoliticalSystem : ISimulationSystem
    {
        public string Name => nameof(PoliticalSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Political;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
        }
    }
}
