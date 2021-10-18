using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenDifferenceScorer : TokenDifferenceScorerBase
    {
        public TokenDifferenceScorer() : base(DefaultRatioStrategy<string>.StringArrInstance)
        {

        }
    }
}
