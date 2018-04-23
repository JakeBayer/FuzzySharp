using System;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public abstract class StrategySensitiveScorerBase : ScorerBase
    {
        protected abstract Func<string, string, int> Scorer { get; }
    }
}
