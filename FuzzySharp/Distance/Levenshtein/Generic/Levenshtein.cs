using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzySharp.Distance.Levenshtein.Generic
{
    public class Levenshtein<TObj> : LevenshteinBase, IDistanceMetric<IEnumerable<TObj>> 
        where TObj : IEquatable<TObj>
    {
        public double GetDistance(IEnumerable<TObj> t1, IEnumerable<TObj> t2) 
        {
            var s1     = t1.ToArray();
            var s2     = t2.ToArray();
            int len1   = s1.Length;
            int len2   = s2.Length;
            int lensum = len1 + len2;

            int editDistance = EditDistance(s1, s2, 1);

            return editDistance == 0 ? 1 : (lensum - editDistance) / (double)lensum;
        }
    }
}
