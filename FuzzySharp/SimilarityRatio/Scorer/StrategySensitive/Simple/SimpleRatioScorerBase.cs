using System;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class SimpleRatioScorerBase : StrategySensitiveScorerBase
    {
        public SimpleRatioScorerBase(Func<string, string, int> scorer) : base(scorer)
        {

        }
        public override int Score(string input1, string input2)
        {
            return Scorer(input1, input2);
        }
    }
}