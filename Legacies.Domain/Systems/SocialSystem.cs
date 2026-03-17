using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class SocialSystem : ISimulationSystem
    {
        public string Name => nameof(SocialSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Social;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
        }
    }
}
