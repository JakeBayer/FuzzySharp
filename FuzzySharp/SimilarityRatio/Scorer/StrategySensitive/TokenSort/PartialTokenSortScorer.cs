using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenSortScorer : TokenSortScorerBase
    {
        public PartialTokenSortScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {

        }
    }
}
