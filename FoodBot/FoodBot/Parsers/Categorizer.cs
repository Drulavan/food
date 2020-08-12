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
            message = DeletePunctuation(message);
            result.AddRange(MorphologicalAnalysis(message.ToUpper()));
            return result;
        }

        private List<string> SplitWords(string message)
        {
            return message.Split()
                .ToList();
        }

        private string DeletePunctuation(string message)
        {
            return new string(message.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        private List<Categories> MorphologicalAnalysis(List<string> words)
        {
            List<Categories> result = new List<Categories>();
            foreach (List<string> list in foodDictionary.Values)
            {
                bool hasMatch = words.Any(words => list.ToList().Any(y => y.ToUpper() == words));
            }

            return result;
        }


        private List<Categories> MorphologicalAnalysis(string words)
        {
            List<Categories> result = new List<Categories>();
            foreach (KeyValuePair<Categories,List<string>> cat in foodDictionary)
            {
                bool hasMatch = cat.Value.Any(tag => Regex.IsMatch(words, @$"\b{tag}\b",RegexOptions.IgnoreCase));
                if (hasMatch)
                {
                    result.Add(cat.Key);
                }
            }

            return result;
        }
    }
}