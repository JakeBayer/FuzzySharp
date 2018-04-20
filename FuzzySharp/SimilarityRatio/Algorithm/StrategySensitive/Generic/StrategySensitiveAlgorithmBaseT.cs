using System;
using System.Collections.Generic;
using FuzzySharp.SimilarityRatio.Algorithm.Generic;
using FuzzySharp.SimilarityRatio.Strategy;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Generic
{
    public abstract class StrategySensitiveAlgorithmBase<T> : AlgorithmBase<T>, IStrategySensitiveAlgorithm<T> where T : IEquatable<T>
    {
        private static readonly Dictionary<RatioStrategyType, IRatioStrategy<T>> s_ratioStrategyMap = new Dictionary<RatioStrategyType, IRatioStrategy<T>>
        {
            [RatioStrategyType.Default] = new DefaultRatioStrategy<T>(),
            [RatioStrategyType.Partial] = new PartialRatioStrategy<T>(),
        };

        internal abstract int Calculate(T[] input1, T[] input2, IRatioStrategy<T> strategy);

        public int Calculate(T[] input1, T[] input2, RatioStrategyType strategyType = RatioStrategyType.Default)
        {
            return Calculate(input1, input2, GetRatioStrategy(strategyType));
        }

        internal static IRatioStrategy<T> GetRatioStrategy(RatioStrategyType strategyType)
        {
            try
            {
                return s_ratioStrategyMap[strategyType];
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Don't know how to use ratio strategy type {strategyType}");
            }
        }
    }
}
