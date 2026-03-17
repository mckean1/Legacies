using Legacies.Domain.Models;

namespace Legacies.Client.Renderers
{
    public class ChronicleRenderer
    {
        private readonly Chronicle _chronicle;

        public ChronicleRenderer(Chronicle chronicle)
        {
            _chronicle = chronicle;

            Console.CursorVisible = false;
        }

        public void Render()
        {
            string output = $"Year: {_chronicle.Year}, Month: {_chronicle.Month}";

            Console.SetCursorPosition(0, 0);
            Console.Write(output.PadRight(Console.WindowWidth));
        }
    }
}
