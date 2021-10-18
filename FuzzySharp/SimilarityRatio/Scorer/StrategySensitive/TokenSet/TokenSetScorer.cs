using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenSetScorer : TokenSetScorerBase
    {
        public TokenSetScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {

        }
    }
}
