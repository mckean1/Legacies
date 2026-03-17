using Legacies.Domain.Models;

namespace Legacies.Client.Renderers
{
    public class ChronicleRenderer
    {
        private readonly Chronicle _chronicle;

        public ChronicleRenderer(Chronicle chronicle)
        {
            _chronicle = chronicle;
        }

        public string Render()
        {
            return $"Year: {_chronicle.Year}, Month: {_chronicle.Month}";
        }
    }
}
