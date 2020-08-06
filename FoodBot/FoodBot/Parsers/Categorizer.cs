using FoodBot.Dal.Models;
using System.Collections.Generic;
using System.Linq;

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

            result.AddRange(MorphologicalAnalysis(message.ToLower()));
            return result;
        }

        private List<string> SplitWords(string message)
        {
            var punctuation = message.Where(char.IsPunctuation).Distinct().ToArray();
            return message.Split()
                .Select(x => x.Trim(punctuation).ToLower())
                .ToList();
        }

        private List<Categories> MorphologicalAnalysis(List<string> words)
        {
            List<Categories> result = new List<Categories>();
            foreach (List<string> list in foodDictionary.Values)
            {
                bool hasMatch = words.Any(x => list.ToList().Any(y => y == x));
            }

            return result;
        }


        private List<Categories> MorphologicalAnalysis(string words)
        {
            List<Categories> result = new List<Categories>();
            foreach (List<string> list in foodDictionary.Values)
            {
                bool hasMatch = list.Any(tag => words.Contains(tag));
            }

            return result;
        }
    }
}