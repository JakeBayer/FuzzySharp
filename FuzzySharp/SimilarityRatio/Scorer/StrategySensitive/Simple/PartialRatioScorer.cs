using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialRatioScorer : SimpleRatioScorerBase
    {
        public static readonly IRatioScorer Instance = new PartialRatioScorer();

        private PartialRatioScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {
        }
    }
}