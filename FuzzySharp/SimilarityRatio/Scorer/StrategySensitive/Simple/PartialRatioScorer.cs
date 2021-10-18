using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialRatioScorer : SimpleRatioScorerBase
    {
        public PartialRatioScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {

        }
    }
}
