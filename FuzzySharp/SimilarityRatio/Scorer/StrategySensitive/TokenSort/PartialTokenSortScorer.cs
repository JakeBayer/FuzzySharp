using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenSortScorer : TokenSortScorerBase
    {
        public static readonly IRatioScorer Instance = new PartialTokenSortScorer();

        private PartialTokenSortScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {
        }
    }
}