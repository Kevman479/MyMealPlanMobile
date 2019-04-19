using System;
using Xamarin.Forms;

namespace MyMealPlanMobile.Models
{
    public class Day
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public Meal Breakfast { get; set; }
        public Meal Lunch { get; set; }
        public Meal Dinner { get; set; }
        public Meal Snack { get; set; }
        public StackLayout Stack { get; set; }
    }
}
