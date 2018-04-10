using System.Linq;
using System.Text.RegularExpressions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Generic;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public class TokenDifferenceAlgorithm : StrategySensitiveAlgorithmBase<string>, IStrategySensitiveAlgorithm<string>, IRatioCalculator
    {
        protected IStringPreprocessor Preprocessor;

        public TokenDifferenceAlgorithm() : base(new DefaultRatioStrategy<string>())
        {
            Preprocessor = new DefaultStringPreprocessor();
        }

        public TokenDifferenceAlgorithm(IStringPreprocessor preprocessor) : base(new DefaultRatioStrategy<string>())
        {
            Preprocessor = preprocessor;
        }

        public TokenDifferenceAlgorithm(IRatioStrategy<string> strategy) : base(strategy)
        {
            Preprocessor = new DefaultStringPreprocessor();
        }

        public TokenDifferenceAlgorithm(IRatioStrategy<string> strategy, IStringPreprocessor preprocessor) : base(strategy)
        {
            Preprocessor = preprocessor;
        }

        public override int Calculate(string[] input1, string[] input2, IRatioStrategy<string> strategy)
        {
            return strategy.Calculate(input1, input2);
        }

        public int Calculate(string input1, string input2) => Calculate(input1, input2, Strategy, Preprocessor);

        public int Calculate(string input1, string input2, IStringPreprocessor preprocessor) => Calculate(input1, input2, Strategy, preprocessor);

        public int Calculate(string input1, string input2, IRatioStrategy<string> strategy) => Calculate(input1, input2, strategy, Preprocessor);

        public int Calculate(string input1, string input2, IRatioStrategy<string> strategy, IStringPreprocessor preprocessor)
        {
            input1 = preprocessor.Process(input1);
            input2 = preprocessor.Process(input2);

            var tokens1 = Regex.Split(input1, @"\s+").OrderBy(s => s).ToArray();
            var tokens2 = Regex.Split(input2, @"\s+").OrderBy(s => s).ToArray();

            return Calculate(tokens1, tokens2, strategy);
        }

        public TokenDifferenceAlgorithm With(IStringPreprocessor stringProcessor)
        {
            SetStringProcessor(stringProcessor);
            return this;
        }

        public TokenDifferenceAlgorithm WithNoProcessor()
        {
            SetStringProcessor(new NoPreprocess());
            return this;
        }

        public void SetStringProcessor(IStringPreprocessor stringProcessor)
        {
            Preprocessor = stringProcessor;
        }

        public IStringPreprocessor GetStringProcessor()
        {
            return Preprocessor;
        }
    }
}
