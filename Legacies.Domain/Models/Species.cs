namespace Legacies.Domain.Models
{
    public sealed class Species
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal MonthlyGrowthRate { get; set; }

        public decimal ScarcityDeclineRate { get; set; }
    }
}
