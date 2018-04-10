using System;

namespace FuzzySharp.SimilarityRatio.Generic
{
    public interface IRatioCalculator<in T> where T : IEquatable<T>
    {
        int Calculate(T[] input1, T[] input2);
    }
}
