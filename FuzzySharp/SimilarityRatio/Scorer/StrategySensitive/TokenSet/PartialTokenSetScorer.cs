using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenSetScorer : TokenSetScorerBase
    {
        public PartialTokenSetScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {

        }

    }
}
