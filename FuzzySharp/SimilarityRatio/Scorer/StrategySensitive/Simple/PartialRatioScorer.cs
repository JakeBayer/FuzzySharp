using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    internal class PartialRatioScorer : SimpleRatioScorerBase<PartialRatioStrategy>
    {
        private static readonly Lazy<PartialRatioScorer> s_lazy = new Lazy<PartialRatioScorer>(() => new PartialRatioScorer());

        public static PartialRatioScorer Instance => s_lazy.Value;

        private PartialRatioScorer() : base(PartialRatioStrategy.Instance)
        {
        }
    }
}
