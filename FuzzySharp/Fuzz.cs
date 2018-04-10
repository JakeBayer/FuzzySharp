using System.Collections.Generic;
using FuzzySharp.Extractor;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Algorithm.Composite;
using FuzzySharp.SimilarityRatio.Algorithm.StrategySensitive;
using FuzzySharp.SimilarityRatio.Strategy;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp
{
    public class Fuzz
    {
        #region Ratio
        /// <summary>
        /// Calculates a Levenshtein simple ratio between the strings.
        /// This indicates a measure of similarity
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int Ratio(string input1, string input2)
        {
            return new SimpleRatioAlgorithm().Calculate(input1, input2);
        }

        /// <summary>
        /// Calculates a Levenshtein simple ratio between the strings.
        /// This indicates a measure of similarity
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int Ratio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new SimpleRatioAlgorithm(preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region PartialRatio
        /// <summary>
        /// Inconsistent substrings lead to problems in matching. This ratio
        /// uses a heuristic called "best partial" for when two strings
        /// are of noticeably different lengths.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int PartialRatio(string input1, string input2)
        {
            return new SimpleRatioAlgorithm(new PartialRatioStrategy()).Calculate(input1, input2);
        }

        /// <summary>
        /// Inconsistent substrings lead to problems in matching. This ratio
        /// uses a heuristic called "best partial" for when two strings
        /// are of noticeably different lengths.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int PartialRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new SimpleRatioAlgorithm(new PartialRatioStrategy(), preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region TokenSortRatio
        /// <summary>
        /// Find all alphanumeric tokens in the string and sort
        /// those tokens and then take ratio of resulting
        /// joined strings.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int TokenSortRatio(string input1, string input2)
        {
            return new TokenSortAlgorithm().Calculate(input1, input2);
        }

        /// <summary>
        /// Find all alphanumeric tokens in the string and sort
        /// those tokens and then take ratio of resulting
        /// joined strings.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenSortRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenSortAlgorithm(preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region TokenSortPartialRatio
        /// <summary>
        /// Find all alphanumeric tokens in the string and sort
        /// those tokens and then take ratio of resulting
        /// joined strings.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int TokenSortPartialRatio(string input1, string input2)
        {
            return new TokenSortAlgorithm(new PartialRatioStrategy()).Calculate(input1, input2);
        }

        /// <summary>
        /// Find all alphanumeric tokens in the string and sort
        /// those tokens and then take ratio of resulting
        /// joined strings.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenSortPartialRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenSortAlgorithm(new PartialRatioStrategy(), preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region TokenSetRatio
        /// <summary>
        /// Splits the strings into tokens and computes intersections and remainders
        /// between the tokens of the two strings.A comparison string is then
        /// built up and is compared using the simple ratio algorithm.
        /// Useful for strings where words appear redundantly.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenSetRatio(string input1, string input2)
        {
            return new TokenSetAlgorithm().Calculate(input1, input2);
        }

        /// <summary>
        /// Splits the strings into tokens and computes intersections and remainders
        /// between the tokens of the two strings.A comparison string is then
        /// built up and is compared using the simple ratio algorithm.
        /// Useful for strings where words appear redundantly.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenSetRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenSetAlgorithm(preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region TokenSetPartialRatio
        /// <summary>
        /// Splits the strings into tokens and computes intersections and remainders
        /// between the tokens of the two strings.A comparison string is then
        /// built up and is compared using the simple ratio algorithm.
        /// Useful for strings where words appear redundantly.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int TokenSetPartialRatio(string input1, string input2)
        {
            return new TokenSetAlgorithm(new PartialRatioStrategy()).Calculate(input1, input2);
        }

        /// <summary>
        /// Splits the strings into tokens and computes intersections and remainders
        /// between the tokens of the two strings.A comparison string is then
        /// built up and is compared using the simple ratio algorithm.
        /// Useful for strings where words appear redundantly.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenSetPartialRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenSetAlgorithm(new PartialRatioStrategy(), preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region TokenDifferenceRatio
        /// <summary>
        /// Splits the strings into tokens and computes the ratio on those tokens (not the individual chars,
        /// but the strings themselves)
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenDifferenceRatio(string input1, string input2)
        {
            return new TokenDifferenceAlgorithm().Calculate(input1, input2);
        }

        /// <summary>
        /// Splits the strings into tokens and computes the ratio on those tokens (not the individual chars,
        /// but the strings themselves)
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenDifferenceRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenDifferenceAlgorithm().Calculate(input1, input2, preprocessor);
        }
        #endregion

        #region TokenDifferencePartialRatio
        /// <summary>
        /// Splits the strings into tokens and computes the ratio on those tokens (not the individual chars,
        /// but the strings themselves)
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int TokenDifferencePartialRatio(string input1, string input2)
        {
            return new TokenDifferenceAlgorithm(new PartialRatioStrategy<string>()).Calculate(input1, input2);
        }

        /// <summary>
        /// Splits the strings into tokens and computes the ratio on those tokens (not the individual chars,
        /// but the strings themselves)
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenDifferencePartialRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenDifferenceAlgorithm(new PartialRatioStrategy<string>(), preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region TokenDifferenceRatio
        /// <summary>
        /// Splits longer string into tokens and takes the initialism and compares it to the shorter
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenInitialismRatio(string input1, string input2)
        {
            return new TokenInitialismAlgorithm().Calculate(input1, input2);
        }

        /// <summary>
        /// Splits longer string into tokens and takes the initialism and compares it to the shorter
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenInitialismRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenInitialismAlgorithm().Calculate(input1, input2, preprocessor);
        }
        #endregion

        #region TokenDifferencePartialRatio
        /// <summary>
        /// Splits longer string into tokens and takes the initialism and compares it to the shorter
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int TokenInitialismPartialRatio(string input1, string input2)
        {
            return new TokenInitialismAlgorithm(new PartialRatioStrategy()).Calculate(input1, input2);
        }

        /// <summary>
        /// Splits longer string into tokens and takes the initialism and compares it to the shorter
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenInitialismPartialRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenInitialismAlgorithm(new PartialRatioStrategy(), preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region TokenDifferenceRatio
        /// <summary>
        /// Similarity ratio that attempts to determine whether one strings tokens are an abbreviation
        /// of the other strings tokens. One string must have all its characters in order in the other string
        /// to even be considered.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenAbbreviationRatio(string input1, string input2)
        {
            return new TokenAbbreviationAlgorithm().Calculate(input1, input2);
        }

        /// <summary>
        /// Similarity ratio that attempts to determine whether one strings tokens are an abbreviation
        /// of the other strings tokens. One string must have all its characters in order in the other string
        /// to even be considered.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenAbbreviationRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenAbbreviationAlgorithm().Calculate(input1, input2, preprocessor);
        }
        #endregion

        #region TokenDifferencePartialRatio
        /// <summary>
        /// Similarity ratio that attempts to determine whether one strings tokens are an abbreviation
        /// of the other strings tokens. One string must have all its characters in order in the other string
        /// to even be considered.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int TokenAbbreviationPartialRatio(string input1, string input2)
        {
            return new TokenAbbreviationAlgorithm(new PartialRatioStrategy()).Calculate(input1, input2);
        }

        /// <summary>
        /// Similarity ratio that attempts to determine whether one strings tokens are an abbreviation
        /// of the other strings tokens. One string must have all its characters in order in the other string
        /// to even be considered.
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int TokenAbbreviationPartialRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new TokenAbbreviationAlgorithm(new PartialRatioStrategy(), preprocessor).Calculate(input1, input2);
        }
        #endregion

        #region WeightedRatio
        /// <summary>
        /// Calculates a weighted ratio between the different algorithms for best results
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static int WeightedRatio(string input1, string input2)
        {
            return new WeightedComparisonAlgorithm().Calculate(input1, input2);
        }

        /// <summary>
        /// Calculates a weighted ratio between the different algorithms for best results
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="preprocessor"></param>
        /// <returns></returns>
        public static int WeightedRatio(string input1, string input2, IStringPreprocessor preprocessor)
        {
            return new WeightedComparisonAlgorithm().Calculate(input1, input2, preprocessor);
        }
        #endregion

            }
}
