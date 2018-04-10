using System.Collections.Generic;
using System.Linq;
using FuzzySharp.Extensions;
using FuzzySharp.SimilarityRatio;

namespace FuzzySharp.Extractor
{
    public class ResultExtractor
    {
        public static IEnumerable<ExtractedResult> ExtractWithoutOrder(string query, IEnumerable<string> choices, IRatioCalculator calculator, int cutoff = 0)
        {
            int index = 0;
            foreach (var choice in choices)
            {
                int score = calculator.Calculate(query, choice);
                if (score >= cutoff)
                {
                    yield return new ExtractedResult(choice, score, index);
                }
                index++;
            }
        }

        public static ExtractedResult ExtractOne(string query, IEnumerable<string> choices, IRatioCalculator calculator, int cutoff = 0)
        {
            return ExtractWithoutOrder(query, choices, calculator, cutoff).Max();
        }

        public static IEnumerable<ExtractedResult> ExtractSorted(string query, IEnumerable<string> choices, IRatioCalculator calculator, int cutoff = 0)
        {
            return ExtractWithoutOrder(query, choices, calculator, cutoff).OrderByDescending(r => r.Score);
        }

        public static IEnumerable<ExtractedResult> ExtractTop(string query, IEnumerable<string> choices, IRatioCalculator calculator, int limit, int cutoff = 0)
        {
            return ExtractWithoutOrder(query, choices, calculator, cutoff).MaxN(limit).Reverse();
        }
    }
}
