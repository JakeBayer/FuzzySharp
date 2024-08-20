using System;
using System.Collections.Generic;

namespace FuzzySharp.Extensions
{
    internal static class StringExtensions
    {
        public static List<string> ExtractTokens(this string input)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(input))
                return result;

            var span = input.AsSpan();

            var start = 0;
            for (var i = 0; i < span.Length; i++)
            {
                if (char.IsLetter(span[i])) continue;

                if (i - start > 0)
                {
                    result.Add(span[start..i].ToString());
                }

                start = i+1;
            }

            if (span.Length - start > 0)
                result.Add(span[start..].ToString());

            return result;
        }

        public static string[] SplitByAnySpace(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return [];

            var words = input.Split(EmptyArray<char>(), StringSplitOptions.RemoveEmptyEntries);

            return words;
        }

        public static string[] GetSortedWords(this string input)
        {
            var words = SplitByAnySpace(input);

            Array.Sort(words);

            return words;
        }

        public static string NormalizeSpacesAndSort(this string input)
        {
            var words = GetSortedWords(input);

            return string.Join(" ", words);
        }

        private static T[] EmptyArray<T>()
        {
            return [];
        }
    }
}
