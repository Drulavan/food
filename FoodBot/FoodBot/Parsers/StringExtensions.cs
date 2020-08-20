using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace FoodBot.Parsers
{
    internal static class StringExtensions
    {
        public static string RemovePunctuation(this string text)
        {
            var replacePunctuation = Regex.Replace(text,
                @"[^\w]+", " ");

            return replacePunctuation.Trim();
        }
    }
}
