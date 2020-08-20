using FoodBot.Dal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FoodBot.Parsers
{
    public class Categorizer
    {
        private readonly Dictionary<Categories, List<string>> foodDictionary;

        public Categorizer(Dictionary<Categories, List<string>> foodDictionary)
        {
            this.foodDictionary = foodDictionary;
        }

        public List<Categories> Categorize(string message)
        {
            var result = new List<Categories>();
            result.AddRange(MorphologicalAnalysis(message.RemovePunctuation().ToUpper()));
            return result;
        }
       
        private List<Categories> MorphologicalAnalysis(string words)
        {
            List<Categories> result = new List<Categories>();
            foreach (KeyValuePair<Categories, List<string>> cat in foodDictionary)
            {
                bool hasMatch = cat.Value.Any(tag => Regex.IsMatch(words, @$"\b{tag}\b", RegexOptions.IgnoreCase));
                if (hasMatch)
                {
                    result.Add(cat.Key);
                }
            }

            return result;
        }
    }
}