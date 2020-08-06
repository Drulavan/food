using System.ComponentModel;

namespace FoodBot.Dal.Models
{
    public enum Categories
    {
        [Description("Рыба")]
        Fish,

        [Description("Мясо")]
        Meat,

        [Description("Выпечка")]
        Bake,

        [Description("Овощи")]
        Vegetables,

        [Description("Фрукты")]
        Fruits,

        [Description("Молочная продукция")]
        Milk,
    }
}