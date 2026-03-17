using Legacies.Domain.Interfaces;

namespace Legacies.Domain.Models
{
    public sealed class SimulationEngine
    {
        private readonly List<RegisteredSystem> _systems = new();
        private int _nextRegistrationOrder;
        private int _tickIndex;

        public World World { get; }

        public SimulationEngine(World world)
        {
            World = world;
        }

        public void RegisterSystem(ISimulationSystem system)
        {
            ArgumentNullException.ThrowIfNull(system);

            _systems.Add(new RegisteredSystem(system, _nextRegistrationOrder));
            _nextRegistrationOrder++;
        }

        public SimulationStepResult Tick(SimulationRunOptions? options = null)
        {
            SimulationRunOptions resolvedOptions = options ?? SimulationRunOptions.Default;
            SimulationContext context = new SimulationContext(_tickIndex, resolvedOptions);
            SimulationStepResult result = new SimulationStepResult(_tickIndex, World.CurrentDate.Clone());

            foreach (RegisteredSystem registeredSystem in GetOrderedSystems())
            {
                registeredSystem.System.Execute(World, context, result);
                result.ExecutedSystems.Add(registeredSystem.System.Name);
            }

            result.EndDate = World.CurrentDate.Clone();
            _tickIndex++;

            return result;
        }

        public IReadOnlyList<SimulationStepResult> RunMonths(int monthCount, SimulationRunOptions? options = null)
        {
            if (monthCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(monthCount));
            }

            List<SimulationStepResult> results = new(monthCount);

            for (int month = 0; month < monthCount; month++)
            {
                results.Add(Tick(options));
            }

            return results;
        }

        private IEnumerable<RegisteredSystem> GetOrderedSystems()
        {
            return _systems
                .OrderBy(system => system.System.Phase)
                .ThenBy(system => system.RegistrationOrder);
        }

        private sealed record RegisteredSystem(ISimulationSystem System, int RegistrationOrder);
    }
}
