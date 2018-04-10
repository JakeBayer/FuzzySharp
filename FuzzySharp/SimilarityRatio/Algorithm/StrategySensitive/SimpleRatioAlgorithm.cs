using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public class SimpleRatioAlgorithm : StrategySensitiveAlgorithmBase
    {
        public SimpleRatioAlgorithm()
        {
        }

        public SimpleRatioAlgorithm(IStringPreprocessor preprocessor) : base(preprocessor)
        {
        }

        public SimpleRatioAlgorithm(IRatioStrategy strategy) : base(strategy)
        {
        }

        public SimpleRatioAlgorithm(IRatioStrategy strategy, IStringPreprocessor preprocessor) : base(strategy, preprocessor)
        {
        }

        public override int Calculate(string input1, string input2, IRatioStrategy strategy, IStringPreprocessor preprocessor)
        {
            input1 = preprocessor.Process(input1);
            input2 = preprocessor.Process(input2);
            return strategy.Calculate(input1, input2);
        }
    }
}
