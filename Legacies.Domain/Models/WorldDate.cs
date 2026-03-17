using Legacies.Domain.Constants;
using Legacies.Domain.Enums;

namespace Legacies.Domain.Models
{
    public sealed class WorldDate
    {
        public int Year { get; private set; }

        public int Month { get; private set; }

        public int AbsoluteMonth { get; private set; }

        public Season Season => Month switch
        {
            >= WorldConstants.FirstMonthOfYear and < WorldConstants.SummerStartMonth => Season.Spring,
            >= WorldConstants.SummerStartMonth and < WorldConstants.AutumnStartMonth => Season.Summer,
            >= WorldConstants.AutumnStartMonth and < WorldConstants.WinterStartMonth => Season.Autumn,
            _ => Season.Winter
        };

        public WorldDate(int year, int month)
        {
            if (year < WorldConstants.FirstYear)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            if (month < WorldConstants.FirstMonthOfYear || month > WorldConstants.MonthsInYear)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            Year = year;
            Month = month;
            AbsoluteMonth = ((year - WorldConstants.FirstYear) * WorldConstants.MonthsInYear) + (month - WorldConstants.FirstMonthOfYear);
        }

        public void AdvanceOneMonth()
        {
            Month++;
            AbsoluteMonth++;

            if (Month > WorldConstants.MonthsInYear)
            {
                Month = WorldConstants.FirstMonthOfYear;
                Year++;
            }
        }

        public WorldDate Clone()
        {
            return new WorldDate(Year, Month);
        }

        public override string ToString()
        {
            return $"Year {Year}, Month {Month}";
        }
    }
}