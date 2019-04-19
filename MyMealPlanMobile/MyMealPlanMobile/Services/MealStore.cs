using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMealPlanMobile.Models;

namespace MyMealPlanMobile.Services
{
    public class MealStore : IDataStore<Meal>
    {
        List<Meal> meals;

        public IDataStore<Recipe> RStore => Xamarin.Forms.DependencyService.Get<IDataStore<Recipe>>();

        public MealStore()
        {
            meals = new List<Meal>();
            var mockMeals = new List<Meal>
            {
                new Meal { Id = Guid.NewGuid().ToString(), Recipes = new List<Recipe>() { RStore.GetItemAsync("Pancakes").Result }, Type = Recipe.RecipeType.Breakfast },
                new Meal { Id = Guid.NewGuid().ToString(), Recipes = new List<Recipe>() { RStore.GetItemAsync("Scrambled Eggs").Result }, Type = Recipe.RecipeType.Lunch },
                new Meal { Id = Guid.NewGuid().ToString(), Recipes = new List<Recipe>() { RStore.GetItemAsync("Cake").Result, RStore.GetItemAsync("Pancakes").Result }, Type = Recipe.RecipeType.Dinner },
                new Meal { Id = Guid.NewGuid().ToString(), Recipes = new List<Recipe>() { RStore.GetItemAsync("Fried Egg").Result }, Type = Recipe.RecipeType.Snack }
            };

            foreach (var meal in mockMeals)
            {
                meals.Add(meal);
            }
        }

        public async Task<bool> AddItemAsync(Meal meal)
        {
            meals.Add(meal);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Meal meal)
        {
            var oldMeal = meals.Where((Meal arg) => arg.Id == meal.Id).FirstOrDefault();
            meals.Remove(oldMeal);
            meals.Add(meal);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldMeal = meals.Where((Meal arg) => arg.Id == id).FirstOrDefault();
            meals.Remove(oldMeal);

            return await Task.FromResult(true);
        }

        public async Task<Meal> GetItemAsync(string id)
        {
            return await Task.FromResult(meals.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Meal>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(meals);
        }
    }
}