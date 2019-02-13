using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    internal class DefaultRatioScorer : SimpleRatioScorerBase<DefaultRatioStrategy>
    {
        private static readonly Lazy<DefaultRatioScorer> s_lazy = new Lazy<DefaultRatioScorer>(() => new DefaultRatioScorer());

        public static DefaultRatioScorer Instance => s_lazy.Value;

        private DefaultRatioScorer() : base(DefaultRatioStrategy.Instance)
        {
        }
    }
}
