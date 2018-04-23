using System;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.TokenDifference
{
    public class TokenDifferenceScorer : TokenDifferenceScorerBase
    {
        protected override Func<string[], string[], int> Scorer => DefaultRatioStrategy<string>.Calculate;
    }
}
