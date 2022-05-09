﻿using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class PartialTokenInitialismScorer : TokenInitialismScorerBase
    {
        protected override Func<string, string, int> Scorer => PartialRatioStrategy.CacheCalculate;
    }
}
