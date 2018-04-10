using System;

namespace FuzzySharp.SimilarityRatio.Strategy.Generic
{
    public class DefaultRatioStrategy<T> : IRatioStrategy<T> where T : IEquatable<T>
    {
        public int Calculate(T[] input1, T[] input2)
        {
            return (int) Math.Round(100 * Levenshtein.GetRatio(input1, input2));
        }
    }
}
