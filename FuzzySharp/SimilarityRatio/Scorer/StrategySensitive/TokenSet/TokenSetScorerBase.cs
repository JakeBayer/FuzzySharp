using System;
using System.Collections.Generic;
using System.Linq;
using FuzzySharp.Extensions;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class TokenSetScorerBase : StrategySensitiveScorerBase
    {
        public override int Score(string input1, string input2)
        {
            var tokens1 = new HashSet<string>(input1.SplitByAnySpace());
            var tokens2 = new HashSet<string>(input2.SplitByAnySpace());

            var intersection = GetIntersectionAndExcept(tokens1, tokens2);

            var sortedIntersection = string.Join(" ", intersection.OrderBy(s => s));
            var sortedDiff1To2     = (sortedIntersection + " " + string.Join(" ", tokens1.OrderBy(s => s))).Trim();
            var sortedDiff2To1     = (sortedIntersection + " " + string.Join(" ", tokens2.OrderBy(s => s))).Trim();

            var score1 = Scorer(sortedIntersection, sortedDiff1To2);
            var score2 = Scorer(sortedIntersection, sortedDiff2To1);
            var score3 = Scorer(sortedDiff1To2, sortedDiff2To1);
            
            return Math.Max(score1, Math.Max(score2, score3));
        }

        private static List<T> GetIntersectionAndExcept<T>(HashSet<T> first, HashSet<T> second)
        {
            List<T> intersection = [];

            foreach (var item in first.ToArray())
            {
                if (second.Remove(item))
                {
                    first.Remove(item);
                    intersection.Add(item);
                }
            }

            return intersection;
        }
    }
}
