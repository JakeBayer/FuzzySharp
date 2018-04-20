namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal interface IRatioStrategy
    {
        int Calculate(string input1, string input2);
    }
}
