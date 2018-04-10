using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.Extractor;
using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Algorithm.Composite;

namespace FuzzySharp
{
    public class Process
    {
        #region ExtractTop
        /// <summary>
        /// Creates a sorted list of ExtractedResult  which contain the
        /// top limit most similar choices
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <param name="calculator"></param>
        /// <param name="limit"></param>
        /// <param name="cutoff"></param>
        /// <returns></returns>
        public static IEnumerable<ExtractedResult> ExtractTop(string query, IEnumerable<string> choices, IRatioCalculator calculator, int limit, int cutoff = 0)
        {
            return ResultExtractor.ExtractTop(query, choices, calculator, limit, cutoff);
        }

        /// <summary>
        /// Creates a sorted list of ExtractedResult  which contain the
        /// top limit most similar choices
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <param name="limit"></param>
        /// <param name="cutoff"></param>
        /// <returns></returns>
        public static IEnumerable<ExtractedResult> ExtractTop(string query, IEnumerable<string> choices, int limit, int cutoff = 0)
        {
            return ResultExtractor.ExtractTop(query, choices, new WeightedComparisonAlgorithm(), limit, cutoff);
        }
        #endregion

        #region ExtractSorted
        /// <summary>
        /// Creates a sorted list of ExtractedResult with the closest matches first
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <param name="calculator"></param>
        /// <param name="cutoff"></param>
        /// <returns></returns>
        public static IEnumerable<ExtractedResult> ExtractSorted(string query, IEnumerable<string> choices, IRatioCalculator calculator, int cutoff = 0)
        {
            return ResultExtractor.ExtractSorted(query, choices, calculator, cutoff);
        }

        /// <summary>
        /// Creates a sorted list of ExtractedResult with the closest matches first
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <param name="cutoff"></param>
        /// <returns></returns>
        public static IEnumerable<ExtractedResult> ExtractSorted(string query, IEnumerable<string> choices, int cutoff = 0)
        {
            return ResultExtractor.ExtractSorted(query, choices, new WeightedComparisonAlgorithm(), cutoff);
        }
        #endregion

        #region ExtractAll
        /// <summary>
        /// Creates a list of ExtractedResult which contain all the choices with
        /// their corresponding score where higher is more similar
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <param name="calculator"></param>
        /// <param name="cutoff"></param>
        /// <returns></returns>
        public static IEnumerable<ExtractedResult> ExtractAll(string query, IEnumerable<string> choices, IRatioCalculator calculator, int cutoff = 0)
        {
            return ResultExtractor.ExtractWithoutOrder(query, choices, calculator, cutoff);
        }

        /// <summary>
        /// Creates a list of ExtractedResult which contain all the choices with
        /// their corresponding score where higher is more similar
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <param name="cutoff"></param>
        /// <returns></returns>
        public static IEnumerable<ExtractedResult> ExtractAll(string query, IEnumerable<string> choices, int cutoff = 0)
        {
            return ResultExtractor.ExtractWithoutOrder(query, choices, new WeightedComparisonAlgorithm(), cutoff);
        }
        #endregion

        #region ExtractOne
        /// <summary>
        /// Find the single best match above a score in a list of choices.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <param name="calculator"></param>
        /// <returns></returns>
        public static ExtractedResult ExtractOne(string query, IEnumerable<string> choices, IRatioCalculator calculator)
        {
            return ResultExtractor.ExtractOne(query, choices, calculator);
        }

        /// <summary>
        /// Find the single best match above a score in a list of choices.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <returns></returns>
        public static ExtractedResult ExtractOne(string query, IEnumerable<string> choices)
        {
            return ResultExtractor.ExtractOne(query, choices, new WeightedComparisonAlgorithm());
        }

        /// <summary>
        /// Find the single best match above a score in a list of choices.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="choices"></param>
        /// <returns></returns>
        public static ExtractedResult ExtractOne(string query, params string[] choices)
        {
            return ResultExtractor.ExtractOne(query, choices, new WeightedComparisonAlgorithm());
        }
        #endregion
    }
}
