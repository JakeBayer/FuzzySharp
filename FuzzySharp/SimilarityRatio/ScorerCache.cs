using System;
using System.Collections.Concurrent;
using FuzzySharp.SimilarityRatio.Scorer;

namespace FuzzySharp.SimilarityRatio
{
    public static class ScorerCache
    {
        private static readonly ConcurrentDictionary<Type, IRatioScorer> s_scorerCache = new ConcurrentDictionary<Type, IRatioScorer>();
        public static IRatioScorer Get<T>() where T : IRatioScorer, new()
        {
            return s_scorerCache.GetOrAdd(typeof(T), new T());
        }
    }
}
