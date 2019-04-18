﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyMealPlanMobile.Models
{
    public enum MenuItemType
    {
        Browse,
        Items,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
