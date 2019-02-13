using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzySharp.Distance
{
    public interface IDistanceMetric<in TObj>
    {
        double GetDistance(TObj t1, TObj t2);
    }
}
