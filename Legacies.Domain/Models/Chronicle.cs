namespace Legacies.Domain.Models
{
    public class Chronicle
    {
        public int Year => _simulationEngine.World.CurrentDate.Year;
        public int Month => _simulationEngine.World.CurrentDate.Month;

        private readonly SimulationEngine _simulationEngine;
        
        public Chronicle(SimulationEngine simulationEngine)
        {
            _simulationEngine = simulationEngine;
        }
    }
}
