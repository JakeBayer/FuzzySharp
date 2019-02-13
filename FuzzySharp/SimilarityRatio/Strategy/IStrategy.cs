using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.Distance;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal interface IStrategy<in TObj, out TDistanceMetric>
        where TDistanceMetric : IDistanceMetric<TObj>
    {
        TDistanceMetric DistanceMetric { get; }
        int Calculate(TObj input1, TObj input2);
    }
}
