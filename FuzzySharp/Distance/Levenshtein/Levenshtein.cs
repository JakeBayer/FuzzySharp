using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FuzzySharp.Edits;

namespace FuzzySharp.Distance.Levenshtein
{
    public class Levenshtein : LevenshteinBase, IDistanceMetric<string>
    {
        private static readonly Lazy<Levenshtein> s_lazy = new Lazy<Levenshtein>(() => new Levenshtein());

        public static Levenshtein Instance => s_lazy.Value;

        private Levenshtein() { }

        

        // Special Case
        public static int EditDistance(string s1, string s2, int xcost = 0)
        {
            return EditDistance(s1.ToCharArray(), s2.ToCharArray(), xcost);
        }





        public static double GetRatio<T>(IEnumerable<T> input1, IEnumerable<T> input2) where T : IEquatable<T>
        {
            var s1 = input1.ToArray();
            var s2 = input2.ToArray();
            int len1 = s1.Length;
            int len2 = s2.Length;
            int lensum = len1 + len2;

            int editDistance = EditDistance(s1, s2, 1);

            return editDistance == 0 ? 1 : (lensum - editDistance) / (double)lensum;
        }

        // Special Case
        public double GetDistance(string t1, string t2)
        {
            return GetRatio(t1.ToCharArray(), t2.ToCharArray());
        }
    }
}
