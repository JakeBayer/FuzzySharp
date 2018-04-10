namespace FuzzySharp.PreProcess
{
    public class NoPreprocess : IStringPreprocessor
    {
        public string Process(string input) => input;
    }
}
