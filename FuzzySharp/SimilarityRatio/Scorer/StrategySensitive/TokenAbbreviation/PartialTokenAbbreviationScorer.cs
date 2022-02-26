using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenAbbreviationScorer : TokenAbbreviationScorerBase
    {
        public static readonly IRatioScorer Instance = new PartialTokenAbbreviationScorer();

        private PartialTokenAbbreviationScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {
        }
    }
}