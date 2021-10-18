using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenInitialismScorer : TokenInitialismScorerBase
    {
        public TokenInitialismScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {

        }
    }
}
