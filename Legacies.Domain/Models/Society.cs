namespace Legacies.Domain.Models
{
    public sealed class Society
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? HomeRegionId { get; set; }
    }
}
