using MyMealPlanMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MyMealPlanMobile.Models
{
    public class RecipeGroup : ObservableCollection<Recipe>, INotifyPropertyChanged
    {
        private bool _expanded;

        public string Title { get; set; }

        public string TitleWithItemCount
        {
            get { return string.Format("{0} ({1})", Title, RecipeCount); }
        }

        public string ShortName { get; set; }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                }
            }
        }

        public string StateIcon
        {
            get { return Expanded ? "RecipeGroupIcon_Expanded.png" : "RecipeGroupIcon_Collapsed.png"; }
        }

        public int RecipeCount { get; set; }

        public Recipe.RecipeType RecipeGroupType;

        public static ObservableCollection<RecipeGroup> RecipeGroups { get; private set; }

        static RecipeGroup()
        {
            ObservableCollection<RecipeGroup> Groups = new ObservableCollection<RecipeGroup>
            {
                new RecipeGroup("Breakfast") { RecipeGroupType = Recipe.RecipeType.Breakfast },
                new RecipeGroup("Lunch") { RecipeGroupType = Recipe.RecipeType.Lunch },
                new RecipeGroup("Dinner") { RecipeGroupType = Recipe.RecipeType.Dinner },
                new RecipeGroup("Snack") { RecipeGroupType = Recipe.RecipeType.Snack },
                new RecipeGroup("Leftovers") { RecipeGroupType = Recipe.RecipeType.Leftovers }
            };
            IDataStore<Recipe> RecipeStore = DependencyService.Get<IDataStore<Recipe>>() ?? new RecipeStore();
            foreach (var Recipe in RecipeStore.GetItemsAsync().Result)
            {
                foreach (var Type in Recipe.Types)
                {
                    foreach (var group in Groups)
                    {
                        if (group.RecipeGroupType == Type)
                        {
                            group.Add(Recipe);
                        }
                    }
                }
            }
            RecipeGroups = Groups;
        }

        public RecipeGroup(string title, string shortName = "", bool expanded = true)
        {
            Title = title;
            ShortName = shortName != "" ? shortName : Title;
            Expanded = expanded;
        }
    }
}
