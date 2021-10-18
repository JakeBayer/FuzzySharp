using System;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal class PartialRatioStrategy<T> where T : IEquatable<T>
    {
        private readonly ILevenshteinMatchingBlocks<T> _levenshteinMatchingBlocks;
        private readonly ILevenshteinRatio<T> _levenshteinRatio;

        internal static readonly Func<string, string, int> StringInstance = (input1, input2) => new PartialRatioStrategy<char>().Calculate(input1.AsSpan(), input2.AsSpan());
        internal static readonly Func<string[], string[], int> StringArrInstance = (input1, input2) => new PartialRatioStrategy<string>().Calculate(input1.AsSpan(), input2.AsSpan());

        internal PartialRatioStrategy() : this(LevenshteinRatio<T>.Instance, LevenshteinMatchingBlocks<T>.Instance)
        {
        }

        internal PartialRatioStrategy(ILevenshteinRatio<T> levenshteinRatio, ILevenshteinMatchingBlocks<T> levenshteinMatchingBlocks)
        {
            _levenshteinRatio = levenshteinRatio;
            _levenshteinMatchingBlocks = levenshteinMatchingBlocks;
        }

        internal int Calculate(ReadOnlySpan<T> input1, ReadOnlySpan<T> input2)
        {
            if (input1.Length == 0 || input2.Length == 0)
            {
                return 0;
            }
            var isInput1LengthLessThanInput2 = input1.Length < input2.Length;
            var shorter = isInput1LengthLessThanInput2 ? input1 : input2;
            var longer = isInput1LengthLessThanInput2 ? input2 : input1;

            var matchingBlocks = _levenshteinMatchingBlocks.GetMatchingBlocks(shorter, longer);

            double? highestRatio = null;
            foreach (var matchingBlock in matchingBlocks)
            {
                int dist = matchingBlock.DestPos - matchingBlock.SourcePos;

                int longStart = dist > 0 ? dist : 0;
                int longEnd = longStart + shorter.Length;

                if (longEnd > longer.Length) longEnd = longer.Length;

                var longSubstr = longer.Slice(longStart, longEnd - longStart);

                double ratio = _levenshteinRatio.GetRatio(shorter, longSubstr);

                if (ratio > .995)
                {
                    return 100;
                }

                if (!highestRatio.HasValue)
                {
                    highestRatio = ratio;
                }
                else
                {
                    highestRatio = Math.Max(highestRatio.Value, ratio);
                }
            }

            return (int)Math.Round(100 * highestRatio ?? 0);
        }
    }
}