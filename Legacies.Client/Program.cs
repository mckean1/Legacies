using Legacies.Client.Renderers;
using Legacies.Domain.Models;

World world = new World();
SimulationEngine simulationEngine = new SimulationEngine(world);

ChronicleRenderer chronicleRenderer = new ChronicleRenderer(new Chronicle(simulationEngine));

while (world.CurrentDate.Year < 10)
{
    simulationEngine.Tick();
    chronicleRenderer.Render();
}