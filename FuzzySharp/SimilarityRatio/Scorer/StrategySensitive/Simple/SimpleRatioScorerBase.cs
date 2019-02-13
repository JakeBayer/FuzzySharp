using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    internal abstract class SimpleRatioScorerBase<TStrategy> : StrategySensitiveScorerBase<TStrategy>
        where TStrategy : IStrategy
    {
        protected SimpleRatioScorerBase(TStrategy strategy) : base(strategy)
        {
        }

        public override int Score(string input1, string input2)
        {
            return Strategy.Calculate(input1, input2);
        }
    }
}
