using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyMealPlanMobile.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public enum RecipeType { Breakfast, Lunch, Dinner, Snack, Leftover }
        public List<RecipeType> Types { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; } = new ObservableCollection<Ingredient>();
        public string Preperation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DiscardOn { get; set; }
    }
}
