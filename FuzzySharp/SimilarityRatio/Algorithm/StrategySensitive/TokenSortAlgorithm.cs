using System;
using System.Linq;
using System.Text.RegularExpressions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public class TokenSortAlgorithm : StrategySensitiveAlgorithmBase
    {
        internal override int Calculate(string input1, string input2, IRatioStrategy strategy)
        {
            var sorted1 = String.Join(" ", Regex.Split(input1, @"\s+").OrderBy(s => s)).Trim();
            var sorted2 = String.Join(" ", Regex.Split(input2, @"\s+").OrderBy(s => s)).Trim();

            return strategy.Calculate(sorted1, sorted2);
        }
    }
}
