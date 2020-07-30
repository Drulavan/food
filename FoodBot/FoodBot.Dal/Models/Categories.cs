using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FoodBot.Dal.Models
{
    public enum Categories
    {
        [Description("Рыба")]
        Fish,

        [Description("Мясо")]
        Meat,
    }
}
