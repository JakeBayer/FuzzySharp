using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Algorithm
{
    public abstract class AlgorithmBase : IRatioCalculator
    {
        internal abstract int RunAlgorithm(string input1, string input2);

        public int Calculate(string input1, string input2, PreprocessMode preprocessMode = PreprocessMode.Full)
        {
            var preprocessor = StringPreprocessorFactory.GetPreprocessor(preprocessMode);
            input1 = preprocessor(input1);
            input2 = preprocessor(input2);
            return RunAlgorithm(input1, input2);
        }
    }
}
