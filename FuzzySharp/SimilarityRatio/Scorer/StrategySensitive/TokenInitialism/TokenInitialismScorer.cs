using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenInitialismScorer : TokenInitialismScorerBase
    {
        public static readonly IRatioScorer Instance = new TokenInitialismScorer();

        private TokenInitialismScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {
        }
    }
}