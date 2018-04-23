using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Scorer
{
    public abstract class ScorerBase : IRatioScorer
    {
        public abstract int Score(string input1, string input2);

        public int Score(string input1, string input2, PreprocessMode preprocessMode)
        {
            var preprocessor = StringPreprocessorFactory.GetPreprocessor(preprocessMode);
            input1 = preprocessor(input1);
            input2 = preprocessor(input2);
            return Score(input1, input2);
        }
    }
}
