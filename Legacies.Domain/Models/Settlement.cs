namespace Legacies.Domain.Models
{
    public sealed class Settlement
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int RegionId { get; set; }

        public int? SocietyId { get; set; }
    }
}
