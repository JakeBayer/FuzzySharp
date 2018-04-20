using System;
using FuzzySharp.SimilarityRatio.Strategy;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Generic
{
    public interface IStrategySensitiveAlgorithm<in T> where T : IEquatable<T>
    {
        int Calculate(T[] input1, T[] input2, RatioStrategyType strategyType = RatioStrategyType.Default);
    }
}
