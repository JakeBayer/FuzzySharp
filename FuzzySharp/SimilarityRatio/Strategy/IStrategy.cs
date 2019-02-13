using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.Distance;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal interface IStrategy<in TObj>
    {
        int Calculate(TObj input1, TObj input2);
    }
}
