using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyMealPlanMobile.Models;
using MyMealPlanMobile.Views;

namespace MyMealPlanMobile.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public Timeframe Timeframe { get; set; }
        public ObservableCollection<Day> Days { get; set; }
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Meal> Meals { get; set; }
        public Command LoadDaysCommand { get; set; }

        public ObservableCollection<RecipeGroup> _allGroups;
        public ObservableCollection<RecipeGroup> _expandedGroups;

        public HomePageViewModel()
        {
            Title = "Home";
            Days = new ObservableCollection<Day>();
            Recipes = new ObservableCollection<Recipe>();
            Ingredients = new ObservableCollection<Ingredient>();
            Meals = new ObservableCollection<Meal>();
            LoadDaysCommand = new Command(async (TargetDay) => await ExecuteLoadDaysCommand((DateTime)TargetDay));
            LoadDaysCommand.Execute(DateTime.Now);
            foreach (var day in DayStore.GetItemsAsync().Result)
            {
                Days.Add(day);
            }
            foreach (var recipe in RecipeStore.GetItemsAsync().Result)
            {
                Recipes.Add(recipe);
            }
            foreach (var ingredient in IngredientStore.GetItemsAsync().Result)
            {
                Ingredients.Add(ingredient);
            }
            foreach (var meal in MealStore.GetItemsAsync().Result)
            {
                Meals.Add(meal);
            }

            MessagingCenter.Subscribe<NewRecipePage, Recipe>(this, "AddRecipe", async (obj, recipe) =>
            {
                var newRecipe = recipe as Recipe;
                Recipes.Add(newRecipe);
                foreach (var Type in newRecipe.Types)
                {
                    foreach (var group in _allGroups)
                    {
                        if (group.RecipeGroupType == Type)
                        {
                            group.Add(newRecipe);
                        }
                    }
                }
                UpdateListContents();
                await RecipeStore.AddItemAsync(newRecipe);
            });

            MessagingCenter.Subscribe<NewIngredientPage, Ingredient>(this, "AddIngredient", async (obj, ingredient) =>
            {
                var newIngredient = ingredient as Ingredient;
                Ingredients.Add(newIngredient);
                await IngredientStore.AddItemAsync(newIngredient);
            });
        }

        public ObservableCollection<RecipeGroup> UpdateListContents()
        {
            _expandedGroups = new ObservableCollection<RecipeGroup>();
            foreach (var group in _allGroups)
            {
                var newGroup = new RecipeGroup(group.Title, group.ShortName, group.Expanded) { RecipeCount = group.Count };
                if (group.Expanded)
                {
                    foreach (var recipe in group)
                    {
                        newGroup.Add(recipe);
                    }
                }
                _expandedGroups.Add(newGroup);
            }
            return _expandedGroups;
        }

        async Task ExecuteLoadDaysCommand(DateTime TargetDay)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (TargetDay == null)
                {
                    TargetDay = DateTime.Now;
                }
                Days.Clear();
                TargetDay = new DateTime(TargetDay.Year, TargetDay.Month, TargetDay.Day);
                Timeframe = new Timeframe(TargetDay);
                for (var i = 0; i < Timeframe.EmptyPadding; i++)
                {
                    Days.Add(new Day());
                }
                var days = await DayStore.GetItemsAsync(true);
                int DaysLeft = Timeframe.DayCount;
                foreach (var day in days)
                {
                    if (day.Date.CompareTo(Timeframe.FirstDay) >= 0 && DaysLeft > 0)
                    {
                        DaysLeft--;
                        Days.Add(day);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}