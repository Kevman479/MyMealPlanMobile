using System;
using System.Collections.Generic;
using System.Text;

namespace MyMealPlanMobile.Models
{
    public class Meal
    {
        public string Id { get; set; }
        public List<Recipe> Recipes { get; set; }
        public Day Day { get; set; }
        public Recipe.RecipeType Type { get; set; }
        public string Description
        {
            get
            {
                var description = "";
                foreach (var recipe in Recipes)
                {
                    description += recipe.Description + ", ";
                }
                return description.Substring(0, description.Length - 3);
            }
        }
    }
}
