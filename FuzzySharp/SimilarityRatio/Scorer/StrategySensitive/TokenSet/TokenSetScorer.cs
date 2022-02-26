using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenSetScorer : TokenSetScorerBase
    {
        public static readonly IRatioScorer Instance = new TokenSetScorer();

        private TokenSetScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {
        }
    }
}