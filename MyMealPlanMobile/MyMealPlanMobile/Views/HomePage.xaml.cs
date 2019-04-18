using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyMealPlanMobile.ViewModels;

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
        }

        int i = 0;
        private void Alert(object sender, EventArgs e)
        {
            i++;
            DisplayAlert("Alert", $"You have been alerted {i} times", "OK");
        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Page", this.GetType().Name, "OK");
        }

        private void DaysListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Days.Count == 0)
            {
                viewModel.LoadDaysCommand.Execute(DateTime.Now);
                MonthNameLabel.Text = viewModel.Timeframe.FirstDay.ToString("MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("en"));
                BuildCalendar();
            }
        }

        private void PreviousMonth_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadDaysCommand.Execute(new DateTime(viewModel.Timeframe.FirstDay.Year, viewModel.Timeframe.FirstDay.Month - 1, 1));
            BuildCalendar();
        }

        private void NextMonth_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadDaysCommand.Execute(new DateTime(viewModel.Timeframe.FirstDay.Year, viewModel.Timeframe.FirstDay.Month + 1, 1));
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
            var CurrentRow = 1;
            var CurrentColumn = 0;
            foreach (var day in viewModel.Days)
            {
                var DayOutline = new StackLayout();
                if (day.Id != null)
                {
                    DayOutline.Children.Add(new Label() { Text = day.Date.Day.ToString() });
                    DayOutline.Children.Add(new Label() { Text = $"Breakfast: {(day.Breakfast != null ? day.Breakfast.Name : "")}" });
                    DayOutline.Children.Add(new Label() { Text = $"Lunch: {(day.Lunch != null ? day.Lunch.Name : "")}" });
                    DayOutline.Children.Add(new Label() { Text = $"Dinner: {(day.Dinner != null ? day.Dinner.Name : "")}" });
                    DayOutline.Children.Add(new Label() { Text = $"Snack: {(day.Snack != null ? day.Snack.Name : "")}" });
                }
                Calendar.Children.Add(DayOutline, CurrentColumn, CurrentRow);
                CurrentColumn = ++CurrentColumn > 6 ? 0 : CurrentColumn;
                CurrentRow = CurrentColumn == 0 ? ++CurrentRow : CurrentRow;
            }
            foreach (RowDefinition Row in Calendar.RowDefinitions)
            {
                Row.Height = GridLength.Auto;
            }
        }
    }
}