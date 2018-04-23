using System;
using FuzzySharp.SimilarityRatio.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.Generic
{
    public abstract class ScorerBase<T> : IRatioScorer<T> where T : IEquatable<T>
    {
        public abstract int Score(T[] input1, T[] input2);
    }
}
