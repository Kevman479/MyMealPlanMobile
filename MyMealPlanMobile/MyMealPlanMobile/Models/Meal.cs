using System;
using System.Collections.Generic;
using System.Text;

namespace MyMealPlanMobile.Models
{
    public class Meal
    {
        public string id { get; set; }
        public string Name { get; set; }
        public enum MealType { Breakfast, Lunch, Dinner, Snack };
        public MealType Type { get; set; }
        public string Prep { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public Dictionary<Ingredient, string> IngredientPrepOverride { get; set; }
    }
}
