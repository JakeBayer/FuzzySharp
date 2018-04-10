using System;
using FuzzySharp.SimilarityRatio.Algorithm.Generic;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Generic
{
    public abstract class StrategySensitiveAlgorithmBase<T> : AlgorithmBase<T>, IStrategySensitiveAlgorithm<T> where T : IEquatable<T>
    {
        protected IRatioStrategy<T> Strategy;
        protected StrategySensitiveAlgorithmBase()
        {
            Strategy = new DefaultRatioStrategy<T>();
        }

        protected StrategySensitiveAlgorithmBase(IRatioStrategy<T> strategy)
        {
            Strategy = strategy;
        }

        public abstract int Calculate(T[] input1, T[] input2, IRatioStrategy<T> strategy);


        public override int Calculate(T[] input1, T[] input2)
        {
            return Calculate(input1, input2, GetRatioStrategy());
        }

        public StrategySensitiveAlgorithmBase<T> With(IRatioStrategy<T> strategy)
        {
            SetRatioStrategy(strategy);
            return this;
        }

        public void SetRatioStrategy(IRatioStrategy<T> strategy)
        {
            Strategy = strategy;
        }

        public IRatioStrategy<T> GetRatioStrategy()
        {
            return Strategy;
        }
    }
}
