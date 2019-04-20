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
    public partial class NewIngredientPage : ContentPage
    {
        public Ingredient Ingredient { get; set; }

        public NewIngredientPage()
        {
            InitializeComponent();

            Ingredient = new Ingredient { Id = Guid.NewGuid().ToString() };

            BindingContext = Ingredient;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddIngredient", Ingredient);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void NewSizeOption_Clicked(object sender, EventArgs e)
        {
            float.TryParse(Amount.Text, out float amount);
            if (amount != 0.0)
            {
                Ingredient.AddSize(new IngredientBase.SizeOption() { Amount = amount, Unit = Unit.Text });
            }
            RebuildSizeGrid();
        }

        private void RebuildSizeGrid()
        {
            SizeGrid.RowDefinitions = new RowDefinitionCollection() { new RowDefinition() { Height = GridLength.Auto } };
            SizeGrid.Children.Clear();
            int row = 0;
            foreach (var Size in Ingredient.Sizes)
            {
                SizeGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                SizeGrid.Children.Add(new Label() { Text = Size.Amount.ToString() }, 0, row);
                SizeGrid.Children.Add(new Label() { Text = Size.Unit }, 1, row);
                var RemoveButton = new Button() { Text = "Remove" };
                RemoveButton.Clicked += (sender, e) => { Ingredient.Sizes.Remove(Size); RebuildSizeGrid(); };
                SizeGrid.Children.Add(RemoveButton, 2, row);
                row++;
            }
        }
    }
}