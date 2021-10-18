using System;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal class DefaultRatioStrategy<T> where T : IEquatable<T>
    {
        internal static readonly Func<string, string, int> StringInstance = (input1, input2) => new DefaultRatioStrategy<char>().Calculate(input1.AsSpan(), input2.AsSpan());
        internal static readonly Func<string[], string[], int> StringArrInstance = (input1, input2) => new DefaultRatioStrategy<string>().Calculate(input1.AsSpan(), input2.AsSpan());
        private readonly ILevenshteinRatio<T> _levenshteinRatio;

        internal DefaultRatioStrategy() : this(LevenshteinRatio<T>.Instance)
        {
        }

        internal DefaultRatioStrategy(ILevenshteinRatio<T> levenshteinRatio)
        {
            _levenshteinRatio = levenshteinRatio;
        }

        internal int Calculate(ReadOnlySpan<T> input1, ReadOnlySpan<T> input2)
        {
            if (input1.Length == 0 || input2.Length == 0)
            {
                return 0;
            }
            return (int)Math.Round(100 * _levenshteinRatio.GetRatio(input1, input2));
        }
    }
}