using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenAbbreviationScorer : TokenAbbreviationScorerBase
    {
        public PartialTokenAbbreviationScorer() : base(PartialRatioStrategy<char>.StringInstance)
        {

        }
    }
}
