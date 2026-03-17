using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class SettlementSystem : ISimulationSystem
    {
        public string Name => nameof(SettlementSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Settlement;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
        }
    }
}
