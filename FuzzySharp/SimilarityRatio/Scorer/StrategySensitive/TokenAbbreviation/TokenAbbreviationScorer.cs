using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenAbbreviationScorer : TokenAbbreviationScorerBase
    {
        public TokenAbbreviationScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {

        }
    }
}
