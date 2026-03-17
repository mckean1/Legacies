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
            >= 1 and <= 3 => Season.Spring,
            >= 4 and <= 6 => Season.Summer,
            >= 7 and <= 9 => Season.Autumn,
            _ => Season.Winter
        };

        public WorldDate(int year, int month)
        {
            if (year < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            if (month < 1 || month > WorldConstants.MonthsInYear)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            Year = year;
            Month = month;
            AbsoluteMonth = ((year - 1) * WorldConstants.MonthsInYear) + (month - 1);
        }

        public void AdvanceOneMonth()
        {
            Month++;
            AbsoluteMonth++;

            if (Month > WorldConstants.MonthsInYear)
            {
                Month = 1;
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