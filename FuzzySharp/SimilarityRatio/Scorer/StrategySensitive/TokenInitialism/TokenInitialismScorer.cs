using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.TokenInitialism
{
    public class TokenInitialismScorer : TokenInitialismScorerBase
    {
        protected override Func<string, string, int> Scorer => DefaultRatioStrategy.Calculate;
    }
}
