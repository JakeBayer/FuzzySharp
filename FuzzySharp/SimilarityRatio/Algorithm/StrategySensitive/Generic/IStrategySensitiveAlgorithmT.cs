using System;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Generic
{
    public interface IStrategySensitiveAlgorithm<T> where T : IEquatable<T>
    {
        int Calculate(T[] input1, T[] input2, IRatioStrategy<T> strategy);
    }
}
