using Legacies.Client.Renderers;
using Legacies.Domain.Models;
using Legacies.Domain.Systems;

World world = CreateSampleWorld();
SimulationEngine simulationEngine = new SimulationEngine(world);
RegisterSystems(simulationEngine);

ChronicleRenderer chronicleRenderer = new ChronicleRenderer();

foreach (SimulationStepResult stepResult in simulationEngine.RunMonths(18))
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

    world.Regions.AddRange(
    [
        new Region
        {
            Id = 1,
            Name = "Green Basin",
            BaseEcologicalSupport = 220,
            BaseEnvironmentalPressure = 0.12m,
            SeasonalVolatility = 0.14m,
            SeasonalPeakMonth = 5
        },
        new Region
        {
            Id = 2,
            Name = "High Steppe",
            BaseEcologicalSupport = 165,
            BaseEnvironmentalPressure = 0.18m,
            SeasonalVolatility = 0.16m,
            SeasonalPeakMonth = 7
        },
        new Region
        {
            Id = 3,
            Name = "River Plain",
            BaseEcologicalSupport = 260,
            BaseEnvironmentalPressure = 0.10m,
            SeasonalVolatility = 0.08m,
            SeasonalPeakMonth = 4
        }
    ]);

    world.Species.AddRange(
    [
        new Species
        {
            Id = 1,
            Name = "Foragers",
            MonthlyGrowthRate = 0.03m,
            ScarcityDeclineRate = 0.80m
        },
        new Species
        {
            Id = 2,
            Name = "Herds",
            MonthlyGrowthRate = 0.04m,
            ScarcityDeclineRate = 0.65m
        }
    ]);

    world.PopulationGroups.AddRange(
    [
        new PopulationGroup
        {
            Id = 1,
            SpeciesId = 1,
            RegionId = 1,
            Size = 170,
            Health = 0.82m
        },
        new PopulationGroup
        {
            Id = 2,
            SpeciesId = 1,
            RegionId = 2,
            Size = 180,
            Health = 0.74m
        },
        new PopulationGroup
        {
            Id = 3,
            SpeciesId = 2,
            RegionId = 3,
            Size = 210,
            Health = 0.86m
        }
    ]);

    return world;
}