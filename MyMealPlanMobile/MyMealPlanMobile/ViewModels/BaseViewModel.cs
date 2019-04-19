using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MyMealPlanMobile.Models;
using MyMealPlanMobile.Services;

namespace MyMealPlanMobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> ItemStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public IDataStore<Day> DayStore => DependencyService.Get<IDataStore<Day>>() ?? new DayStore();
        public IDataStore<Ingredient> IngredientStore => DependencyService.Get<IDataStore<Ingredient>>() ?? new IngredientStore();
        public IDataStore<Recipe> RecipeStore => DependencyService.Get<IDataStore<Recipe>>() ?? new RecipeStore();
        public IDataStore<Meal> MealStore => DependencyService.Get<IDataStore<Meal>>() ?? new MealStore();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
