using System;
using FuzzySharp.Distance;
using FuzzySharp.Distance.Levenshtein;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal abstract class DefaultRatioStrategyBase<TObj> : RatioStrategyBase<TObj[]>
    {
        protected DefaultRatioStrategyBase(IDistanceMetric<TObj[]> distanceMetric) : base(distanceMetric)
        {
        }

        public override int Calculate(TObj[] input1, TObj[] input2)
        {
            return (int)DistanceMetric.GetDistance(input1, input2);
        }
    }

    internal class DefaultRatioStrategy : RatioStrategyBase<string>
    {
        private static readonly Lazy<DefaultRatioStrategy> s_lazy = new Lazy<DefaultRatioStrategy>(() => new DefaultRatioStrategy());

        public static DefaultRatioStrategy Instance => s_lazy.Value;

        protected DefaultRatioStrategy() : base(Levenshtein.Instance)
        {
        }

        public override int Calculate(string input1, string input2)
        {
            throw new NotImplementedException();
        }
    }
}
