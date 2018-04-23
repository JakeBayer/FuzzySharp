using System;
using FuzzySharp.SimilarityRatio.Scorer.Generic;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive.Generic
{
    public abstract class StrategySensitiveScorerBase<T> : ScorerBase<T> where T : IEquatable<T>
    {
        protected abstract Func<T[], T[], int> Scorer { get; }
    }
}
