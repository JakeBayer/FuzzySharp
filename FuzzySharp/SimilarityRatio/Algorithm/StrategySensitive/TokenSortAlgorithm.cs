using System;
using System.Linq;
using System.Text.RegularExpressions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public class TokenSortAlgorithm : StrategySensitiveAlgorithmBase
    {
        public TokenSortAlgorithm(IStringPreprocessor preprocessor) : base(preprocessor) { }

        public TokenSortAlgorithm()
        { }

        public TokenSortAlgorithm(IRatioStrategy strategy) : base(strategy)
        {
        }

        public TokenSortAlgorithm(IRatioStrategy strategy, IStringPreprocessor preprocessor) : base(strategy, preprocessor)
        {
        }

        public override int Calculate(string input1, string input2, IRatioStrategy strategy, IStringPreprocessor preprocessor)
        {
            input1 = preprocessor.Process(input1);
            input2 = preprocessor.Process(input2);

            var sorted1 = String.Join(" ", Regex.Split(input1, @"\s+").OrderBy(s => s)).Trim();
            var sorted2 = String.Join(" ", Regex.Split(input2, @"\s+").OrderBy(s => s)).Trim();

            return strategy.Calculate(sorted1, sorted2);
        }
    }
}
