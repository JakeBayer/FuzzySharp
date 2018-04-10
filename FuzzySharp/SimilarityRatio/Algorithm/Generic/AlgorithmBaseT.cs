using System;
using FuzzySharp.SimilarityRatio.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.Generic
{
    public abstract class AlgorithmBase<T> : IRatioCalculator<T> where T : IEquatable<T>
    {
        public abstract int Calculate(T[] input1, T[] input2);
    }
}
