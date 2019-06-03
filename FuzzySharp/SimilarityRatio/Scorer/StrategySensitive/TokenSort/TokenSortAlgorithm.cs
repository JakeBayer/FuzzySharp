using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class TokenSortScorerBase : StrategySensitiveScorerBase
    {
        public override int Score(string input1, string input2)
        {
            var sorted1 = String.Join(" ", Regex.Split(input1, @"\s+").Where(s => s.Any()).OrderBy(s => s)).Trim();
            var sorted2 = String.Join(" ", Regex.Split(input2, @"\s+").Where(s => s.Any()).OrderBy(s => s)).Trim();

            return Scorer(sorted1, sorted2);
        }
    }
}
