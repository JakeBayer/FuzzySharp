using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenDifferenceScorer : TokenDifferenceScorerBase
    {
        public static readonly IRatioScorer Instance = new TokenDifferenceScorer();

        private TokenDifferenceScorer() : base(DefaultRatioStrategy<string>.StringArrInstance)
        {
        }
    }
}