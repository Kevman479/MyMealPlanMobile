using System;

namespace MyMealPlanMobile.Models
{
    public class Day
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string DisplayDate
        {
            get
            {
                if (Date != null)
                {
                    return Date.Day.ToString();
                }
                return "";
            }
        }
        public string Text { get; set; }
        public string Description { get; set; }
        public Meal Breakfast { get; set; }
        public Meal Lunch { get; set; }
        public Meal Dinner { get; set; }
        public Meal Snack { get; set; }
    }
}
