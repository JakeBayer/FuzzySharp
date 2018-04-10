using System;
using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    public class DefaultRatioStrategy : IRatioStrategy
    {
        public int Calculate(string input1, string input2)
        {
            return (int)Math.Round(100 * Levenshtein.GetRatio(input1, input2));
        }
    }
}
