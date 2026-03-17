using Legacies.Domain.Constants;

namespace Legacies.Domain.Models
{
    public sealed class PopulationGroup
    {
        public int Id { get; set; }

        public int SpeciesId { get; set; }

        public int RegionId { get; set; }

        public int Size { get; set; }

        public decimal SupportPressure { get; set; }

        public decimal Health { get; set; } = PopulationConstants.DefaultHealth;

        public int? SocietyId { get; set; }
    }
}
