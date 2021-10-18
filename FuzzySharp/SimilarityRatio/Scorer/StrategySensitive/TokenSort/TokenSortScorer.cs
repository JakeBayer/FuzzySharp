using FuzzySharp.SimilarityRatio.Strategy;
using System;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class TokenSortScorer : TokenSortScorerBase
    {
        public TokenSortScorer() : base(DefaultRatioStrategy<char>.StringInstance)
        {

        }        
    }
}
