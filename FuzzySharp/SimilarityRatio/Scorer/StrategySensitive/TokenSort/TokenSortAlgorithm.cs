using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class TokenSortScorerBase : StrategySensitiveScorerBase
    {
        private static readonly Regex SpaceRegex = new Regex(@"\s+", RegexOptions.Compiled); 
        
        public override int Score(string input1, string input2)
        {
            var sorted1 = String.Join(" ", SpaceRegex.Split(input1).Where(s => s.Any()).OrderBy(s => s)).Trim();
            var sorted2 = String.Join(" ", SpaceRegex.Split(input2).Where(s => s.Any()).OrderBy(s => s)).Trim();

            return Scorer(sorted1, sorted2);
        }
    }
}
