using Legacies.Domain.Constants;

namespace Legacies.Domain.Models
{
    public class SimulationEngine
    {
        public World World { get; set; }

        public SimulationEngine(World world)
        {
            World = world;
        }

        public void Tick()
        {
            World.AdvanceTime();
            Thread.Sleep(SimulationEngineConstants.DelayInMilliseconds);
        }
    }
}
