using System;
using System.Linq;
using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Algorithm.Composite
{
    public class WeightedComparisonAlgorithm : AlgorithmBase
    {
        public static double UNBASE_SCALE  = .95;
        public static double PARTIAL_SCALE = .90;
        public static bool TRY_PARTIALS = true;

        public override int Calculate(string input1, string input2, IStringPreprocessor preprocessor)
        {
            input1 = preprocessor.Process(input1);
            input2 = preprocessor.Process(input2);

            int len1 = input1.Length;
            int len2 = input2.Length;

            if (len1 == 0 || len2 == 0) { return 0; }

            bool tryPartials = TRY_PARTIALS;
            double unbaseScale  = UNBASE_SCALE;
            double partialScale = PARTIAL_SCALE;

            int    baseRatio = Fuzz.Ratio(input1, input2);
            double lenRatio  = ((double)Math.Max(len1, len2)) / Math.Min(len1, len2);

            // if strings are similar length don't use partials
            if (lenRatio < 1.5) tryPartials = false;

            // if one string is much shorter than the other
            if (lenRatio > 8) partialScale = .6;

            if (tryPartials)
            {
                double partial    = Fuzz.PartialRatio(input1, input2, preprocessor) * partialScale;
                double partialSor = Fuzz.TokenSortPartialRatio(input1, input2, preprocessor) * unbaseScale * partialScale;
                double partialSet = Fuzz.TokenSetPartialRatio(input1, input2, preprocessor) * unbaseScale * partialScale;
                double partialInitial = Fuzz.TokenInitialismRatio(input1, input2, preprocessor) * unbaseScale * partialScale;
                //double partialDiff = Hair.TokenDifferenceRatio(input1, input2) * unbaseScale * partialScale;

                return (int) Math.Round(new[] { baseRatio, partial, partialSor, partialSet, partialInitial }.Max());
            }
            else
            {
                double tokenSort = Fuzz.TokenSortRatio(input1, input2, preprocessor) * unbaseScale;
                double tokenSet  = Fuzz.TokenSetRatio(input1, input2, preprocessor) * unbaseScale;
                //double diffSet = Hair.TokenDifferenceRatio(input1, input2) * unbaseScale;

                return (int) Math.Round(new[] { baseRatio, tokenSort, tokenSet }.Max());
            }
        }
    }
}
