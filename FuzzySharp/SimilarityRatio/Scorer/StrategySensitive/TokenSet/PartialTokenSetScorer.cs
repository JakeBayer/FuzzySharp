using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenSetScorer : TokenSetScorerBase
    {
        public static readonly IRatioScorer Instance = new PartialTokenSetScorer();

        private PartialTokenSetScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {
        }
    }
}