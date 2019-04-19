using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MyMealPlanMobile.Models
{
    public class IngredientBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<SizeOption> Sizes = new List<SizeOption>();

        public void AddSize(SizeOption size)
        {
            Sizes.Add(size);
        }

        public class SizeOption
        {
            public float Amount { get; set; }
            public string Unit { get; set; }
        }
    }
}
