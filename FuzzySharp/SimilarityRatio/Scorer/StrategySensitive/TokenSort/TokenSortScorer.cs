using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenSortScorer : TokenSortScorerBase
    {
        public static readonly IRatioScorer Instance = new TokenSortScorer();

        private TokenSortScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {
        }
    }
}