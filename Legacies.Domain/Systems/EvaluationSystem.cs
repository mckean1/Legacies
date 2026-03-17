using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class EvaluationSystem : ISimulationSystem
    {
        public string Name => nameof(EvaluationSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Evaluation;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
        }
    }
}
