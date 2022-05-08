using System;
using System.Text.RegularExpressions;

namespace FuzzySharp.PreProcess
{
    internal class StringPreprocessorFactory
    {
        private static readonly Regex FullMatchRegex = new Regex("[^ a-zA-Z0-9]", RegexOptions.Compiled);
        
        private static readonly Func<string, string> CacheDefault = Default;
        private static readonly Func<string, string> Ident = x => x;

        private static string Default(string input)
        {
            input = FullMatchRegex.Replace(input, " ");;
            input = input.ToLower();

            return input.Trim();
        }

        public static Func<string, string> GetPreprocessor(PreprocessMode mode)
        {
            switch (mode)
            {
                case PreprocessMode.Full:
                    return CacheDefault;
                case PreprocessMode.None:
                    return Ident;
                default:
                    throw new InvalidOperationException($"Invalid string preprocessor mode: {mode}");
            }
        }
    }
}
