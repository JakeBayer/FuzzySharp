using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public interface IStrategySensitiveAlgorithm : IRatioCalculator
    {
        int Calculate(string input1, string input2, PreprocessMode preprocessMode = PreprocessMode.Full, RatioStrategyType strategyType = RatioStrategyType.Default);
    }
}
