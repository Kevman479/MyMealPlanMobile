using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyMealPlanMobile.Models;

namespace MyMealPlanMobile.Services
{
    public class RecipeStore : IDataStore<Recipe>
    {
        List<Recipe> recipes;
        public IDataStore<Ingredient> IStore => Xamarin.Forms.DependencyService.Get<IDataStore<Ingredient>>();

        public RecipeStore()
        {
            Ingredient Flour = IStore.GetItemAsync("Flour").Result;
            Ingredient Eggs = IStore.GetItemAsync("Eggs").Result;
            Ingredient Milk = IStore.GetItemAsync("Milk").Result;
            recipes = new List<Recipe>();
            var mockRecipes = new List<Recipe>
            {
                new Recipe { Id = "Pancakes", Types = new List<Recipe.RecipeType>() { Recipe.RecipeType.Breakfast }, Name="Pancakes", Ingredients = new ObservableCollection<Ingredient>() { Flour } },
                new Recipe { Id = "Scrambled Eggs", Types = new List<Recipe.RecipeType>() { Recipe.RecipeType.Lunch }, Name="Scrambled Eggs", Ingredients = new ObservableCollection<Ingredient>() { Eggs, Milk } },
                new Recipe { Id = "Cake", Types = new List<Recipe.RecipeType>() { Recipe.RecipeType.Dinner }, Name="Cake", Ingredients = new ObservableCollection<Ingredient>() { Flour, Eggs, Milk } },
                new Recipe { Id = "Fried Egg", Types = new List<Recipe.RecipeType>() { Recipe.RecipeType.Snack }, Name="Fried Egg", Ingredients = new ObservableCollection<Ingredient>() { Eggs } }
            };

            foreach (var recipe in mockRecipes)
            {
                recipes.Add(recipe);
            }
        }

        public async Task<bool> AddItemAsync(Recipe recipe)
        {
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Recipe recipe)
        {
            var oldItem = recipes.Where((Recipe arg) => arg.Id == recipe.Id).FirstOrDefault();
            recipes.Remove(oldItem);
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = recipes.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            recipes.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Recipe> GetItemAsync(string id)
        {
            return await Task.FromResult(recipes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(recipes);
        }
    }
}