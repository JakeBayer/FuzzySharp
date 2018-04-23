using System;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal class DefaultRatioStrategy
    {
        public static int Calculate(string input1, string input2)
        {
            return (int)Math.Round(100 * Levenshtein.GetRatio(input1, input2));
        }
    }
}
