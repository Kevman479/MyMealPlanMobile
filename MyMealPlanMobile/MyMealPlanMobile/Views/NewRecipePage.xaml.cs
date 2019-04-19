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

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
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