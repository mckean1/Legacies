using Legacies.Domain.Enums;
using Legacies.Domain.Interfaces;
using Legacies.Domain.Models;

namespace Legacies.Domain.Systems
{
    public sealed class CalendarSystem : ISimulationSystem
    {
        public string Name => nameof(CalendarSystem);

        public SimulationSystemPhase Phase => SimulationSystemPhase.Calendar;

        public void Execute(World world, SimulationContext context, SimulationStepResult result)
        {
            world.AdvanceTime();
        }
    }
}
