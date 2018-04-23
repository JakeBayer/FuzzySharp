using System;
using FuzzySharp.SimilarityRatio.Algorithm.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Generic
{
    public abstract class StrategySensitiveScorerBase<T> : ScorerBase<T> where T : IEquatable<T>
    {
        protected abstract Func<T[], T[], int> Scorer { get; }
    }
}
