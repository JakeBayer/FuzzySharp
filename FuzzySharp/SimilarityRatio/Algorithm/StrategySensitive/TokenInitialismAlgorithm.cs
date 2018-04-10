using System.Linq;
using System.Text.RegularExpressions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    public class TokenInitialismAlgorithm : StrategySensitiveAlgorithmBase
    {
        public TokenInitialismAlgorithm()
        {
        }

        public TokenInitialismAlgorithm(IStringPreprocessor preprocessor) : base(preprocessor)
        {
        }

        public TokenInitialismAlgorithm(IRatioStrategy strategy) : base(strategy)
        {
        }

        public TokenInitialismAlgorithm(IRatioStrategy strategy, IStringPreprocessor preprocessor) : base(strategy, preprocessor)
        {
        }

        public override int Calculate(string input1, string input2, IRatioStrategy strategy, IStringPreprocessor preprocessor)
        {
            input1 = preprocessor.Process(input1);
            input2 = preprocessor.Process(input2);

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
