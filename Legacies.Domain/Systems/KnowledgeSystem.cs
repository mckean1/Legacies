using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class KnowledgeSystem : ISimulationSystem
    {
        public string Name => nameof(KnowledgeSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Knowledge;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
        }
    }
}
