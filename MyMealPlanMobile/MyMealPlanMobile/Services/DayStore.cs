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
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 1) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 2) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 3) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 4) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 5) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 6) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 7) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 8) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 9) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 10) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 11) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 12) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 13) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 14) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 15) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 16) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 17) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 18) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 19) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 20) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 21) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 22) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 23) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 24) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 25) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 26) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 27) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 28) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 29) },
                new Day { Id = Guid.NewGuid().ToString(), Date = new DateTime(2019, 4, 30) }
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