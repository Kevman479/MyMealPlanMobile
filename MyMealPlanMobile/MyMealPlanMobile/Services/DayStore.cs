using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMealPlanMobile.Models;

namespace MyMealPlanMobile.Services
{
    public class DayStore : IDataStore<Day>
    {
        List<Day> days;

        public DayStore()
        {
            days = new List<Day>();
            var mockDays = new List<Day>
            {
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 1), Text = "April 1" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 2), Text = "April 2" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 3), Text = "April 3" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 4), Text = "April 4" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 5), Text = "April 5" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 6), Text = "April 6" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 7), Text = "April 7" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 8), Text = "April 8" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 9), Text = "April 9" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 10), Text = "April 10" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 11), Text = "April 11" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 12), Text = "April 12" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 13), Text = "April 13" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 14), Text = "April 14" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 15), Text = "April 15" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 16), Text = "April 16" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 17), Text = "April 17" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 18), Text = "April 18" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 19), Text = "April 19" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 20), Text = "April 20" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 21), Text = "April 21" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 22), Text = "April 22" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 23), Text = "April 23" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 24), Text = "April 24" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 25), Text = "April 25" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 26), Text = "April 26" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 27), Text = "April 27" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 28), Text = "April 28" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 29), Text = "April 29" },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 30), Text = "April 30" }
            };

            foreach (var day in mockDays)
            {
                days.Add(day);
            }
        }

        public async Task<bool> AddItemAsync(Day day)
        {
            days.Add(day);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Day day)
        {
            var oldItem = days.Where((Day arg) => arg.Id == day.Id).FirstOrDefault();
            days.Remove(oldItem);
            days.Add(day);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = days.Where((Day arg) => arg.Id == id).FirstOrDefault();
            days.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Day> GetItemAsync(string id)
        {
            return await Task.FromResult(days.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Day>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(days);
        }
    }
}