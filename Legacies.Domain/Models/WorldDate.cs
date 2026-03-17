using Legacies.Domain.Constants;

namespace Legacies.Domain.Models
{
    public class WorldDate
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public WorldDate(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public void AdvanceOneMonth()
        {
            Month++;

            if (Month > WorldConstants.MonthsInYear)
            {
                Month = 1;
                Year++;
            }
        }
    }
}