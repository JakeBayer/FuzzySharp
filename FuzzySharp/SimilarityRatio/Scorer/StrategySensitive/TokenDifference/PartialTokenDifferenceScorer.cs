using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenDifferenceScorer : TokenDifferenceScorerBase
    {
        public PartialTokenDifferenceScorer() : base(PartialRatioStrategy<string>.StringArrInstance)
        {

        }
    }
}
