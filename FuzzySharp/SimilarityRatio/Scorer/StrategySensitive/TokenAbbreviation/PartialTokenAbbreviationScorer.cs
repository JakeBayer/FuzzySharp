using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.TokenAbbreviation
{
    public class PartialTokenAbbreviationScorer : TokenAbbreviationScorerBase
    {
        protected override Func<string, string, int> Scorer => PartialRatioStrategy.Calculate;
    }
}
