using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public class DefaultRatioScorer : SimpleRatioScorerBase
    {
        public DefaultRatioScorer(): base(DefaultRatioStrategy<char>.StringInstance)
        {

        }        
    }
}
