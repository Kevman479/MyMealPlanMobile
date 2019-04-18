using System;
using System.Collections.Generic;

namespace MyMealPlanMobile.Models
{
    public class Timeframe
    {
        public DateTime FirstDay { get; set; }
        public int DayCount { get; set; }
        public int EmptyPadding = 0;
        private DateTime Day;

        public Timeframe(DateTime day)
        {
            Day = day;
            if (Day.Month == 2)
            {
                if (Day.Year % 4 == 0)
                {
                    DayCount = 29;
                }
                else
                {
                    DayCount = 28;
                }
            }
            else if (Day.Month == 4 || Day.Month == 6 || Day.Month == 9 || Day.Month == 11)
            {
                DayCount = 30;
            }
            else
            {
                DayCount = 31;
            }
            FirstDay = GetFirstDay(Day);
        }

        private DateTime GetFirstDay(DateTime Day)
        {
            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>()
            {
                DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday
            };
            EmptyPadding = daysOfWeek.IndexOf(Day.DayOfWeek);
            return new DateTime(Day.Year, Day.Month, 1);
        }
    }
}
