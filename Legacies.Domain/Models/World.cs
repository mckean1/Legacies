namespace Legacies.Domain.Models
{
    public sealed class World
    {
        public WorldDate CurrentDate { get; set; }

        public List<Region> Regions { get; } = new();

        public List<Species> Species { get; } = new();

        public List<PopulationGroup> PopulationGroups { get; } = new();

        public List<Society> Societies { get; } = new();

        public List<Settlement> Settlements { get; } = new();

        public List<ChronicleEvent> ChronicleHistory { get; } = new();

        public World()
        {
            CurrentDate = new WorldDate(1, 1);
        }

        public void AdvanceTime()
        {
            CurrentDate.AdvanceOneMonth();
        }

        public Region GetRegion(int regionId)
        {
            return Regions.First(region => region.Id == regionId);
        }

        public Species GetSpecies(int speciesId)
        {
            return Species.First(species => species.Id == speciesId);
        }
    }
}
