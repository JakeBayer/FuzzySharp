using System;
using FuzzySharp.SimilarityRatio.Scorer.Generic;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive.Generic
{
    public abstract class StrategySensitiveScorerBase<T> : ScorerBase<T> where T : IEquatable<T>
    {
        protected StrategySensitiveScorerBase(Func<T[], T[], int> scorer)
        {
            Scorer = scorer;
        }
        protected readonly Func<T[], T[], int> Scorer;
    }
}
