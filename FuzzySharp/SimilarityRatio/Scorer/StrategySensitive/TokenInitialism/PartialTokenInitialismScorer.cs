using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenInitialismScorer : TokenInitialismScorerBase
    {
        public PartialTokenInitialismScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {

        }
    }
}
