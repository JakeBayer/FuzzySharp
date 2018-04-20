using System;
using System.Collections.Generic;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public abstract class StrategySensitiveAlgorithmBase : IStrategySensitiveAlgorithm
    {
        private static readonly Dictionary<RatioStrategyType, IRatioStrategy> s_ratioStrategyMap = new Dictionary<RatioStrategyType, IRatioStrategy>
        {
            [RatioStrategyType.Default] = new DefaultRatioStrategy(),
            [RatioStrategyType.Partial] = new PartialRatioStrategy(),
        };

        internal abstract int Calculate(string input1, string input2, IRatioStrategy strategy);

        public int Calculate(string input1, string input2, PreprocessMode preprocessMode = PreprocessMode.Full, RatioStrategyType strategyType = RatioStrategyType.Default)
        {
            var preprocessor = StringPreprocessorFactory.GetPreprocessor(preprocessMode);
            input1 = preprocessor(input1);
            input2 = preprocessor(input2);
            return Calculate(input1, input2, strategyType);
        }

        public int Calculate(string input1, string input2, RatioStrategyType strategyType = RatioStrategyType.Default)
        {
            return Calculate(input1, input2, GetRatioStrategy(strategyType));
        }

        private static IRatioStrategy GetRatioStrategy(RatioStrategyType strategyType)
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

        public int Calculate(string input1, string input2, PreprocessMode preprocessMode = PreprocessMode.Full)
        {
            throw new NotImplementedException();
        }
    }
}
