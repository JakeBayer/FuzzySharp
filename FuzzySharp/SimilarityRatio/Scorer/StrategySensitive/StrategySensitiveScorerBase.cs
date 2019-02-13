using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    internal abstract class StrategySensitiveScorerBase<TStrategy> : ScorerBase
        where TStrategy : IStrategy
    {
        protected StrategySensitiveScorerBase(TStrategy strategy)
        {
            Strategy = strategy;
        }
        protected TStrategy Strategy { get; }
    }
}
