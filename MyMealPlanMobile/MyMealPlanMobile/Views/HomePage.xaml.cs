using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyMealPlanMobile.ViewModels;
using MyMealPlanMobile.Models;

namespace MyMealPlanMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HomePageViewModel();

            viewModel._allGroups = RecipeGroup.RecipeGroups;
            GroupedView.ItemsSource = viewModel.UpdateListContents();
        }

        async private void AddItem_Clicked(object sender, EventArgs e)
        {
            var ItemTypes = new string[] { "Recipe", "Ingredient" };
            string NewItemType = await DisplayActionSheet("Select new item type.", "Cancel", null, ItemTypes);
            if (NewItemType == "Recipe")
            {
                await Navigation.PushModalAsync(new NavigationPage(new NewRecipePage()));
            }
            else if (NewItemType == "Ingredient")
            {
                await Navigation.PushModalAsync(new NavigationPage(new NewIngredientPage()));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MonthNameLabel.Text = viewModel.Timeframe.FirstDay.ToString("MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("en"));
            BuildCalendar();
        }

        private void PreviousMonth_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadDaysCommand.Execute(new DateTime(viewModel.Timeframe.FirstDay.Year, viewModel.Timeframe.FirstDay.Month, 1).AddMonths(-1));
            MonthNameLabel.Text = viewModel.Timeframe.FirstDay.ToString("MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("en"));
            BuildCalendar();
        }

        private void NextMonth_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadDaysCommand.Execute(new DateTime(viewModel.Timeframe.FirstDay.Year, viewModel.Timeframe.FirstDay.Month, 1).AddMonths(1));
            MonthNameLabel.Text = viewModel.Timeframe.FirstDay.ToString("MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("en"));
            BuildCalendar();
        }

        private void BuildCalendar()
        {
            Calendar.Children.Clear();
            Calendar.RowDefinitions.Clear();

            Calendar.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            var RowCount = Math.Ceiling(((viewModel.Timeframe.DayCount + viewModel.Timeframe.EmptyPadding) / 7.0));
            for (var i = 0; i < RowCount; i++)
            {
                Calendar.RowDefinitions.Add(new RowDefinition());
            }
            var CurrentColumn = 0;
            var DayNames = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            foreach (var DayName in DayNames)
            {
                var L = new Label() { Text = DayName };
                var F = new Frame() { BorderColor = Color.Black, BackgroundColor = Color.Gray, Content = L };
                Calendar.Children.Add(F, CurrentColumn, 0);
                CurrentColumn++;
            }
            var CurrentRow = 1;
            CurrentColumn = 0;
            foreach (var day in viewModel.Days)
            {
                if (day.Id != null)
                {
                    var DayOutline = new StackLayout();
                    day.Stack = DayOutline;
                    var DayTitleLabel = new Label() { Text = day.Date.Day.ToString(), ClassId = $"{day.Id}", HorizontalTextAlignment = TextAlignment.Center };
                    var BreakfastLabel = new Label() { Text = day.Breakfast != null ? day.Breakfast.Description : "Breakfast", ClassId = $"{day.Id}.Breakfast" };
                    var LunchLabel = new Label() { Text = day.Lunch != null ? day.Lunch.Description : "Lunch", ClassId = $"{day.Id}.Lunch" };
                    var DinnerLabel = new Label() { Text = day.Dinner != null ? day.Dinner.Description : "Dinner", ClassId = $"{day.Id}.Dinner" };
                    var SnackLabel = new Label() { Text = day.Snack != null ? day.Snack.Description : "Snack", ClassId = $"{day.Id}.Snack" };

                    var DayTitle = new Frame() { Content = DayTitleLabel, ClassId = $"{day.Id}" };
                    var Breakfast = new Frame() { BorderColor = Color.Black, BackgroundColor = Color.Gray, ClassId = $"{day.Id}.Breakfast", Content = BreakfastLabel };
                    var Lunch = new Frame() { BorderColor = Color.Black, BackgroundColor = Color.Gray, ClassId = $"{day.Id}.Lunch", Content = LunchLabel };
                    var Dinner = new Frame() { BorderColor = Color.Black, BackgroundColor = Color.Gray, ClassId = $"{day.Id}.Dinner", Content = DinnerLabel };
                    var Snack = new Frame() { BorderColor = Color.Black, BackgroundColor = Color.Gray, ClassId = $"{day.Id}.Snack", Content = SnackLabel };

                    DayOutline.Children.Add(DayTitle);
                    DayOutline.Children.Add(Breakfast);
                    DayOutline.Children.Add(Lunch);
                    DayOutline.Children.Add(Dinner);
                    DayOutline.Children.Add(Snack);

                    SetTapRecognizer(day, DayOutline);

                    SetTapRecognizer(day, DayTitle);
                    SetTapRecognizer(day, Breakfast);
                    SetTapRecognizer(day, Lunch);
                    SetTapRecognizer(day, Dinner);
                    SetTapRecognizer(day, Snack);
                    SetTapRecognizer(day, DayTitleLabel);
                    SetTapRecognizer(day, BreakfastLabel);
                    SetTapRecognizer(day, LunchLabel);
                    SetTapRecognizer(day, DinnerLabel);
                    SetTapRecognizer(day, SnackLabel);

                    var DayFrame = new Frame() { BorderColor = Color.Black, BackgroundColor = Color.Gray, Content = DayOutline, ClassId = $"{day.Id}" };
                    SetTapRecognizer(day, DayFrame);
                    Calendar.Children.Add(DayFrame, CurrentColumn, CurrentRow);
                }

                CurrentColumn = ++CurrentColumn > 6 ? 0 : CurrentColumn;
                CurrentRow = CurrentColumn == 0 ? ++CurrentRow : CurrentRow;
            }
            foreach (RowDefinition Row in Calendar.RowDefinitions)
            {
                Row.Height = GridLength.Auto;
            }
            var P = 0;
            foreach (ColumnDefinition Column in Calendar.ColumnDefinitions)
            {
                Column.Width = 200;
            }
        }

        private void SetTapRecognizer(Models.Day Day, View Element)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                DayTapped(Day, Element);
            };

            Element.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void DayTapped(Day Day, View Element)
        {
            DisplayAlert("", Element.ClassId, "Ok");
        }

        private void Header_Tapped(object sender, EventArgs e)
        {
            int selectedIndex = viewModel._expandedGroups.IndexOf(((RecipeGroup)((Button)sender).CommandParameter));
            viewModel._allGroups[selectedIndex].Expanded = !viewModel._allGroups[selectedIndex].Expanded;
            GroupedView.ItemsSource = viewModel.UpdateListContents();
        } 
    }
}