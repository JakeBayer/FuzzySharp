using System;
using FuzzySharp.Edits;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal static class PartialRatioStrategy
    {
        public static int Calculate(string input1, string input2)
        {
            if (input1.Length == 0 || input2.Length == 0)
            {
                return 0;
            }

            ReadOnlySpan<char> shorter;
            ReadOnlySpan<char> longer;

            if (input1.Length < input2.Length)
            {
                shorter = input1.AsSpan();
                longer  = input2.AsSpan();
            }
            else
            {
                shorter = input2.AsSpan();
                longer  = input1.AsSpan();
            }

            MatchingBlock[] matchingBlocks = Levenshtein.GetMatchingBlocks(shorter, longer);

            double maxScore = 0;

            foreach (var matchingBlock in matchingBlocks)
            {
                int dist = matchingBlock.DestPos - matchingBlock.SourcePos;

                int longStart = dist > 0 ? dist : 0;
                int longEnd   = longStart + shorter.Length;

                if (longEnd > longer.Length) longEnd = longer.Length;

                var longSubstr = longer[longStart..longEnd];

                double ratio = Levenshtein.GetRatio(shorter, longSubstr);

                if (ratio > .995)
                {
                    return 100;
                }

                if (ratio > maxScore)
                {
                    maxScore = ratio;
                }
            }

            return (int)Math.Round(100 * maxScore);
        }
    }
}
