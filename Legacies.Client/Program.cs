using Legacies.Client.Constants;
using Legacies.Client.Renderers;
using Legacies.Domain.Models;
using Legacies.Domain.Systems;

World world = CreateSampleWorld();
SimulationEngine simulationEngine = new SimulationEngine(world);
RegisterSystems(simulationEngine);

ChronicleRenderer chronicleRenderer = new ChronicleRenderer();

foreach (SimulationStepResult stepResult in simulationEngine.RunMonths(ProgramConstants.SampleSimulationMonths))
{
    chronicleRenderer.Render(stepResult);
}

static void RegisterSystems(SimulationEngine simulationEngine)
{
    simulationEngine.RegisterSystem(new CalendarSystem());
    simulationEngine.RegisterSystem(new EnvironmentSystem());
    simulationEngine.RegisterSystem(new EcologySystem());
    simulationEngine.RegisterSystem(new PopulationSystem());
    simulationEngine.RegisterSystem(new MovementSystem());
    simulationEngine.RegisterSystem(new SocialSystem());
    simulationEngine.RegisterSystem(new SettlementSystem());
    simulationEngine.RegisterSystem(new KnowledgeSystem());
    simulationEngine.RegisterSystem(new InteractionSystem());
    simulationEngine.RegisterSystem(new PoliticalSystem());
    simulationEngine.RegisterSystem(new ChronicleSystem());
    simulationEngine.RegisterSystem(new EvaluationSystem());
}

static World CreateSampleWorld()
{
    World world = new World();

    world.Regions.AddRange(SampleWorldConstants.CreateRegions());
    world.Species.AddRange(SampleWorldConstants.CreateSpecies());
    world.PopulationGroups.AddRange(SampleWorldConstants.CreatePopulationGroups());

    return world;
}