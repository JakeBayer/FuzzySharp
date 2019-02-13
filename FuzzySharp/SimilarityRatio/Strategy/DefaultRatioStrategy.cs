using System;
using FuzzySharp.Distance;
using FuzzySharp.Distance.Levenshtein;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal class DefaultRatioStrategy : IStrategy<string, Levenshtein>
    {

    }

    internal class DefaultRatioStrategy<TDistanceMetric> : IStrategy<string, TDistanceMetric> 
        where TDistanceMetric : IDistanceMetric<string>
    {
        private static readonly Lazy<DefaultRatioStrategy> s_lazy = new Lazy<DefaultRatioStrategy>(() => new DefaultRatioStrategy());

        public static DefaultRatioStrategy Instance => s_lazy.Value;

        private DefaultRatioStrategy()
        {
            DistanceMetric = 
        }

        public TDistanceMetric DistanceMetric { get; }

        public int Calculate(string input1, string input2)
        {
            return (int)Math.Round(100 * Levenshtein.GetRatio(input1, input2));
        }
    }
}
