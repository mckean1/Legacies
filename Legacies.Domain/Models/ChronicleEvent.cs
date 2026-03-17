using Legacies.Domain.Enums;

namespace Legacies.Domain.Models
{
    public sealed class ChronicleEvent
    {
        public ChronicleEvent(
            WorldDate date,
            ChronicleEventCategory category,
            ChronicleEventImportance importance,
            string message)
        {
            Date = date;
            Category = category;
            Importance = importance;
            Message = message;
        }

        public WorldDate Date { get; }

        public ChronicleEventCategory Category { get; }

        public ChronicleEventImportance Importance { get; }

        public string Message { get; }

        public int? RegionId { get; init; }

        public int? PopulationGroupId { get; init; }

        public int? SpeciesId { get; init; }

        public int? SocietyId { get; init; }

        public int? SettlementId { get; init; }
    }
}
