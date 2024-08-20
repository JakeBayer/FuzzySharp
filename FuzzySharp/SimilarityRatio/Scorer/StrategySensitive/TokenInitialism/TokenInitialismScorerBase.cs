using System.Linq;
using FuzzySharp.Extensions;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class TokenInitialismScorerBase : StrategySensitiveScorerBase
    {
        public override int Score(string input1, string input2)
        {
            string shorter;
            string longer;

            if (input1.Length < input2.Length)
            {
                shorter = input1;
                longer  = input2;
            }
            else
            {
                shorter = input2;
                longer  = input1;
            }

            double lenRatio = ((double)longer.Length) / shorter.Length;

            // if longer isn't at least 3 times longer than the other, then it's probably not an initialism
            if (lenRatio < 3) return 0;

            var initials = longer.SplitByAnySpace().Select(s => s[0]).ToArray();

            return Scorer(new string(initials), shorter);
        }
    }
}
