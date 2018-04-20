using System.Linq;
using System.Text.RegularExpressions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive.Generic;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive
{
    //public class TokenDifferenceAlgorithm : StrategySensitiveAlgorithmBase<string>, IStrategySensitiveAlgorithm<string>, IRatioCalculator
    //{
    //    public override int Calculate(string[] input1, string[] input2, IRatioStrategy<string> strategy)
    //    {
    //        return strategy.Calculate(input1, input2);
    //    }

    //    public int Calculate(string input1, string input2, IRatioStrategy<string> strategy)
    //    {
    //        var tokens1 = Regex.Split(input1, @"\s+").OrderBy(s => s).ToArray();
    //        var tokens2 = Regex.Split(input2, @"\s+").OrderBy(s => s).ToArray();

    //        return Calculate(tokens1, tokens2, strategy);
    //    }


    //    public int Calculate(string input1, string input2, PreprocessMode preprocessMode = PreprocessMode.Full)
    //    {
    //        var preprocessor = StringPreprocessorFactory.GetPreprocessor(preprocessMode);
    //        input1 = preprocessor(input1);
    //        input2 = preprocessor(input2);

    //        Calculate(input1, input2);
    //    }
    //}
}
