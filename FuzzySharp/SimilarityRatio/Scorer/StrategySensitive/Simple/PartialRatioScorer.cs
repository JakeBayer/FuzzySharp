using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Simple
{
    public class PartialRatioScorer : SimpleRatioScorerBase
    {
        protected override Func<string, string, int> Scorer => PartialRatioStrategy.Calculate;
    }
}
