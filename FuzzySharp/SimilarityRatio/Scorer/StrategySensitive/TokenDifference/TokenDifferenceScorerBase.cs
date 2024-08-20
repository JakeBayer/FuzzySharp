using FuzzySharp.Extensions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive.Generic;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class TokenDifferenceScorerBase : StrategySensitiveScorerBase<string>, IRatioScorer
    {
        public override int Score(string[] input1, string[] input2)
        {
            return Scorer(input1, input2);
        }

        public int Score(string input1, string input2)
        {
            var tokens1 = input1.GetSortedWords();
            var tokens2 = input2.GetSortedWords();

            return Score(tokens1, tokens2);
        }


        public int Score(string input1, string input2, PreprocessMode preprocessMode)
        {
            var preprocessor = StringPreprocessorFactory.GetPreprocessor(preprocessMode);
            input1 = preprocessor(input1);
            input2 = preprocessor(input2);

            return Score(input1, input2);
        }
    }
}
