using System;

namespace FuzzySharp.PreProcess
{
    internal static class StringPreprocessorFactory
    {
        private static string Default(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            var result = new char[input.Length].AsSpan();

            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                result[i] = char.IsLetterOrDigit(c) ? char.ToLower(c) : ' ';
            }

            return ((ReadOnlySpan<char>)result).Trim().ToString();
        }

        public static Func<string, string> GetPreprocessor(PreprocessMode mode)
        {
            return mode switch
            {
                PreprocessMode.Full => Default,
                PreprocessMode.None => static s => s,
                _ => throw new InvalidOperationException($"Invalid string preprocessor mode: {mode}")
            };
        }
    }
}
