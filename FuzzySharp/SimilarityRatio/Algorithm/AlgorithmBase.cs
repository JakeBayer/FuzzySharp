using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Algorithm
{
    public abstract class AlgorithmBase : IRatioCalculator
    {
        protected IStringPreprocessor Preprocessor;

        protected AlgorithmBase()
        {
            Preprocessor = new DefaultStringPreprocessor();
            
        }

        protected AlgorithmBase(IStringPreprocessor preprocessor)
        {
            Preprocessor = preprocessor;
        }

        public int Calculate(string input1, string input2) => Calculate(input1, input2, Preprocessor);
        public abstract int Calculate(string input1, string input2, IStringPreprocessor preprocessor);

        public AlgorithmBase With(IStringPreprocessor stringProcessor)
        {
            SetStringProcessor(stringProcessor);
            return this;
        }

        public AlgorithmBase WithNoProcessor()
        {
            SetStringProcessor(new NoPreprocess());
            return this;
        }

        protected virtual void SetStringProcessor(IStringPreprocessor stringProcessor)
        {
            Preprocessor = stringProcessor;
        }

        public IStringPreprocessor GetStringProcessor()
        {
            return Preprocessor;
        }
    }
}
