using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

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

    public static class CatCateg
    {
        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }
}