using System;

namespace FuzzySharp.SimilarityRatio.Strategy.Generic
{
    internal class DefaultRatioStrategy<T> where T : IEquatable<T>
    {
        public static int Calculate(T[] input1, T[] input2)
        {
            return (int) Math.Round(100 * Levenshtein.GetRatio(input1, input2));
        }
    }
}
