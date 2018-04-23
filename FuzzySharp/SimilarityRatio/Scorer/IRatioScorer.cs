using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Scorer
{
    public interface IRatioScorer
    {
        int Score(string input1, string input2);
        int Score(string input1, string input2, PreprocessMode preprocessMode);
    }
}
