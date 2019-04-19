using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMealPlanMobile.Models;

namespace MyMealPlanMobile.Services
{
    public class IngredientStore : IDataStore<Ingredient>
    {
        List<Ingredient> ingredients;

        public IngredientStore()
        {
            ingredients = new List<Ingredient>();
            var mockIngredients = new List<Ingredient>
            {
                new Ingredient { Id = "Flour", Name = "Flour", Description = "All purpose wheat flour" },
                new Ingredient { Id = "Eggs", Name = "Eggs", Description = "Chicken eggs" },
                new Ingredient { Id = "Milk", Name = "Milk", Description = "Cow's milk" },
            };

            foreach (var ingredient in mockIngredients)
            {
                ingredients.Add(ingredient);
            }
        }

        public async Task<bool> AddItemAsync(Ingredient ingredient)
        {
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Ingredient ingredient)
        {
            var oldItem = ingredients.Where((Ingredient arg) => arg.Id == ingredient.Id).FirstOrDefault();
            ingredients.Remove(oldItem);
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = ingredients.Where((Ingredient arg) => arg.Id == id).FirstOrDefault();
            ingredients.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Ingredient> GetItemAsync(string id)
        {
            return await Task.FromResult(ingredients.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Ingredient>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(ingredients);
        }
    }
}