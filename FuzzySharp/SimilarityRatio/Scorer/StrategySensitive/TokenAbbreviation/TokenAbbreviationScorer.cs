using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.TokenAbbreviation
{
    public class TokenAbbreviationScorer : TokenAbbreviationScorerBase
    {
        protected override Func<string, string, int> Scorer => DefaultRatioStrategy.Calculate;
    }
}
