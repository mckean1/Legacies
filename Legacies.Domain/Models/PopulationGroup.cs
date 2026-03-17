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

        public decimal MovementPressure { get; set; }

        public decimal DisplacementPressure { get; set; }

        public decimal OpportunityPressure { get; set; }

        public decimal Health { get; set; } = PopulationConstants.DefaultHealth;

        public int? HomeRegionId { get; set; }

        public int? LastMoveAbsoluteMonth { get; set; }

        public int? SocietyId { get; set; }
    }
}
