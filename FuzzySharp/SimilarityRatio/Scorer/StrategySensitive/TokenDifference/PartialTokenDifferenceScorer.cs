using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenDifferenceScorer : TokenDifferenceScorerBase
    {
        public static readonly IRatioScorer Instance = new PartialTokenDifferenceScorer();

        private PartialTokenDifferenceScorer() : base(PartialRatioStrategy<string>.StringArrInstance)
        {
        }
    }
}