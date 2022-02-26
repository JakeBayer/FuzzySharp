using System;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal class DefaultRatioStrategy
    {
        public static int Calculate(string input1, string input2)
        {
            if (input1.Length == 0 || input2.Length == 0)
            {
                return 0;
            }
            return (int)Math.Round(100 * Levenshtein.GetRatio(input1, input2));
        }
    }
}
