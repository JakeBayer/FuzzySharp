using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Simple
{
    public class DefaultRatioScorer : SimpleRatioScorerBase
    {
        protected override Func<string, string, int> Scorer => DefaultRatioStrategy.Calculate;
    }
}
