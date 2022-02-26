using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class DefaultRatioScorer : SimpleRatioScorerBase
    {
        public static readonly IRatioScorer Instance = new DefaultRatioScorer();

        private DefaultRatioScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {
        }
    }
}