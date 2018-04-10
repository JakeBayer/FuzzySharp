using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public abstract class StrategySensitiveAlgorithmBase : AlgorithmBase, IStrategySensitiveAlgorithm
    {
        protected IRatioStrategy Strategy;
        protected StrategySensitiveAlgorithmBase()
        {
            Strategy = new DefaultRatioStrategy();
        }

        protected StrategySensitiveAlgorithmBase(IStringPreprocessor preprocessor) : base(preprocessor)
        {
            Strategy = new DefaultRatioStrategy();
        }

        protected StrategySensitiveAlgorithmBase(IRatioStrategy strategy)
        {
            Strategy = strategy;
        }

        protected StrategySensitiveAlgorithmBase(IRatioStrategy strategy, IStringPreprocessor preprocessor) : base(preprocessor)
        {
            Strategy = strategy;
        }

        public abstract int Calculate(string input1, string input2, IRatioStrategy strategy, IStringPreprocessor preprocessor);

        public int Calculate(string input1, string input2, IRatioStrategy strategy)
        {
            return Calculate(input1, input2, strategy, GetStringProcessor());
        }

        public override int Calculate(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return Calculate(input1, input2, GetRatioStrategy(), preprocessor);
        }

        public StrategySensitiveAlgorithmBase With(IRatioStrategy strategy)
        {
            SetRatioStrategy(strategy);
            return this;
        }

        public void SetRatioStrategy(IRatioStrategy strategy)
        {
            Strategy = strategy;
        }

        public IRatioStrategy GetRatioStrategy()
        {
            return Strategy;
        }
    }
}
