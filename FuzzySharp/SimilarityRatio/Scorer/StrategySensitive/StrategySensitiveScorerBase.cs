using System;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class StrategySensitiveScorerBase : ScorerBase
    {
        public StrategySensitiveScorerBase(Func<string, string, int> scorer)
        {
            Scorer = scorer;
        }

        protected Func<string, string, int> Scorer;
    }
}
