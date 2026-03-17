namespace Legacies.Domain.Models
{
    public class World
    {
        public WorldDate CurrentDate { get; set; }
        public List<Region> Regions { get; set; }
        public List<Species> Species { get; set; }
        public List<PopulationGroup> PopulationGroups { get; set; }
        public List<Society> Societies { get; set; }
        public List<Settlement> Settlements { get; set; }

        public World()
        {
            int year = 1;
            int month = 1;
            CurrentDate = new WorldDate(year, month);

            Regions = new List<Region>();
            Species = new List<Species>();
            PopulationGroups = new List<PopulationGroup>();
            Societies = new List<Society>();
            Settlements = new List<Settlement>();
        }

        public void AdvanceTime()
        {
            CurrentDate.AdvanceOneMonth();
        }
    }
}
