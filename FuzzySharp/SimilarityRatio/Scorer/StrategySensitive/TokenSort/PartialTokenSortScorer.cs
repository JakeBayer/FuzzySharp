using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.TokenSort
{
    public class PartialTokenSortScorer : TokenSortScorerBase
    {
        protected override Func<string, string, int> Scorer => PartialRatioStrategy.Calculate;
    }
}
