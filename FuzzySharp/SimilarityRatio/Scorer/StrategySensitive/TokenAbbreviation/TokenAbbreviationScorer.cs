using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenAbbreviationScorer : TokenAbbreviationScorerBase
    {
        public static readonly IRatioScorer Instance = new TokenAbbreviationScorer();

        private TokenAbbreviationScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {
        }
    }
}