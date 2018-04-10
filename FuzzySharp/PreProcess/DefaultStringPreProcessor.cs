using System.Text.RegularExpressions;

namespace FuzzySharp.PreProcess
{
    public class DefaultStringPreprocessor : IStringPreprocessor
    {
        private static string pattern = "[^ a-zA-Z0-9]";

        public string Process(string input)
        {
            input = Regex.Replace(input, pattern, " ");
            input = input.ToLower();

            return input.Trim();
        }
    }
}
