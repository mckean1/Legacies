namespace Legacies.Client.Renderers
{
    public sealed class ChronicleRenderer
    {
        public void Render(Legacies.Domain.Models.SimulationStepResult stepResult)
        {
            Console.WriteLine($"{stepResult.StartDate} -> {stepResult.EndDate} ({stepResult.EndDate.Season})");
            Console.WriteLine($"Systems: {string.Join(" -> ", stepResult.ExecutedSystems)}");

            if (stepResult.ChronicleEvents.Count == 0)
            {
                Console.WriteLine("Chronicle: none");
            }
            else
            {
                Console.WriteLine("Chronicle:");

                foreach (Legacies.Domain.Models.ChronicleEvent chronicleEvent in stepResult.ChronicleEvents)
                {
                    Console.WriteLine($"- [{chronicleEvent.Category}] {chronicleEvent.Message}");
                }
            }

            Console.WriteLine();
        }
    }
}
