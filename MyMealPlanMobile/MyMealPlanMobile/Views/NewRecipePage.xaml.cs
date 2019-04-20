using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyMealPlanMobile.Models;

namespace MyMealPlanMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class NewRecipePage : ContentPage
    {
        public Recipe Recipe { get; set; }

        public NewRecipePage()
        {
            InitializeComponent();

            Recipe = new Recipe { Id = Guid.NewGuid().ToString() };

            BindingContext = Recipe;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Recipe.Types = new List<Recipe.RecipeType>();
            if (Breakfast.IsToggled)
            {
                Recipe.Types.Add(Recipe.RecipeType.Breakfast);
            }
            if (Lunch.IsToggled)
            {
                Recipe.Types.Add(Recipe.RecipeType.Lunch);
            }
            if (Dinner.IsToggled)
            {
                Recipe.Types.Add(Recipe.RecipeType.Dinner);
            }
            if (Snack.IsToggled)
            {
                Recipe.Types.Add(Recipe.RecipeType.Snack);
            }
            if (Leftovers.IsToggled)
            {
                Recipe.Types.Add(Recipe.RecipeType.Leftovers);
            }
            MessagingCenter.Send(this, "AddRecipe", Recipe);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Leftovers_Toggled(object sender, ToggledEventArgs e)
        {
            LeftoverDates.IsVisible = Leftovers.IsToggled;
        }
    }
}