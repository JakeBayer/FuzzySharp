using System;
using System.Collections.Generic;
using System.Linq;
using FuzzySharp.Extensions;
using FuzzySharp.SimilarityRatio.Scorer;

namespace FuzzySharp.Extractor
{
    public static class ResultExtractor
    {
        public static IEnumerable<ExtractedResult<T>> ExtractWithoutOrder<T>(string query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer scorer, int cutoff = 0)
        {
            int index = 0;
            foreach (var choice in choices)
            {
                int score = scorer.Score(query, processor(choice));
                if (score >= cutoff)
                {
                    yield return new ExtractedResult<T>(choice, score, index);
                }
                index++;
            }
        }

        public static IEnumerable<ExtractedResult<T>> ExtractWithoutOrder<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer scorer, int cutoff = 0)
        {
            var processedQuery = processor(query);
            return ExtractWithoutOrder(processedQuery, choices, processor, scorer, cutoff);
        }

        public static ExtractedResult<T> ExtractOne<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer calculator, int cutoff = 0)
        {
            return ExtractWithoutOrder(query, choices, processor, calculator, cutoff).Max();
        }

        public static IEnumerable<ExtractedResult<T>> ExtractSorted<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer calculator, int cutoff = 0)
        {
            return ExtractWithoutOrder(query, choices, processor, calculator, cutoff).OrderByDescending(r => r.Score);
        }

        public static IEnumerable<ExtractedResult<T>> ExtractTop<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer calculator, int limit, int cutoff = 0)
        {
            return ExtractWithoutOrder(query, choices, processor, calculator, cutoff).MaxN(limit).Reverse();
        }
    }
}
