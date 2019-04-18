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
        public Command LoadDaysCommand { get; set; }

        public HomePageViewModel()
        {
            Title = "Home";
            Days = new ObservableCollection<Day>();
            LoadDaysCommand = new Command(async (TargetDay) => await ExecuteLoadDaysCommand((DateTime)TargetDay));
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