using System.Linq;
using System.Text.RegularExpressions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public class TokenInitialismAlgorithm : StrategySensitiveAlgorithmBase
    {
        internal override int Calculate(string input1, string input2, IRatioStrategy strategy)
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

            var initials = Regex.Split(longer, @"\s+").Select(s => s[0]);

            return strategy.Calculate(string.Join("", initials), shorter);
        }
    }
}
