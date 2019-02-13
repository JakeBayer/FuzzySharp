using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.Distance;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal abstract class RatioStrategyBase<TObj> : IStrategy<TObj>
    {
        protected IDistanceMetric<TObj> DistanceMetric;

        protected RatioStrategyBase(IDistanceMetric<TObj> distanceMetric)
        {
            DistanceMetric = distanceMetric;
        }

        protected int Calculate<T>(T input1, T input2)
        {
            return (int) DistanceMetric.GetDistance(input1, input2);
        }
    }
}
