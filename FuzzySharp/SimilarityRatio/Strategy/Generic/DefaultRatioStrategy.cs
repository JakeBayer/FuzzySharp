using System;
using System.Collections.Generic;
using FuzzySharp.Distance;

namespace FuzzySharp.SimilarityRatio.Strategy.Generic
{
    internal class DefaultRatioStrategy<TObj, TDistanceMetric> : IStrategy<IEnumerable<TObj>, TDistanceMetric> 
        where TObj : IEquatable<TObj> 
        where TDistanceMetric : IDistanceMetric<IEnumerable<TObj>>
    {
        private DefaultRatioStrategy(TDistanceMetric distanceMetric)
        {
            DistanceMetric = distanceMetric;
        }

        public TDistanceMetric DistanceMetric { get; }

        public int Calculate(IEnumerable<TObj> input1, IEnumerable<TObj> input2)
        {
            return (int) Math.Round(100 * DistanceMetric.GetDistance(input1, input2));
        }
    }
}
