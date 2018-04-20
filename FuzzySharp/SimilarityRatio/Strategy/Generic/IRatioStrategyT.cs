using System;
using FuzzySharp.SimilarityRatio.Generic;

namespace FuzzySharp.SimilarityRatio.Strategy.Generic
{
    internal interface IRatioStrategy<in T> : IRatioCalculator<T> where T : IEquatable<T>
    {
    }
}
