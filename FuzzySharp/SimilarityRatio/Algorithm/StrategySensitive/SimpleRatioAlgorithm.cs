using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public class SimpleRatioAlgorithm : StrategySensitiveAlgorithmBase
    {
        internal override int Calculate(string input1, string input2, IRatioStrategy strategy)
        {
            return strategy.Calculate(input1, input2);
        }
    }
}
