using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using FuzzySharp.SimilarityRatio.Scorer;

namespace FuzzySharp.SimilarityRatio
{
    public static class ScorerCache
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRatioScorer Get<T>() where T : IRatioScorer, new() => GenericCache<T>.Instance;

        private static class GenericCache<T> 
            where T : IRatioScorer, new()
        {
            public static readonly T Instance = new T();
        }
    }
}
