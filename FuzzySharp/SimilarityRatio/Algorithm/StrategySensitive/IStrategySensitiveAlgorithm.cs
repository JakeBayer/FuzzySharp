using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public interface IStrategySensitiveAlgorithm : IRatioCalculator
    {
        int Calculate(string input1, string input2, IRatioStrategy strategy);

        int Calculate(string input1, string input2, IRatioStrategy strategy, IStringPreprocessor preprocessor);
    }
}
