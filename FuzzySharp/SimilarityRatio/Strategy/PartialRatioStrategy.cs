using System;
using System.Collections.Generic;
using System.Linq;
using FuzzySharp.Distance;
using FuzzySharp.Distance.Levenshtein;
using FuzzySharp.Edits;
using FuzzySharp.MatchingBlocks;

namespace FuzzySharp.SimilarityRatio.Strategy
{
    internal abstract class PartialRatioStrategyBase<TObj> : RatioStrategyBase<TObj>
    {
        protected PartialRatioStrategyBase(IDistanceMetric<TObj> distanceMetric) : base(distanceMetric)
        {
        }

        private static readonly Lazy<Generic.PartialRatioStrategy<>> s_lazy = new Lazy<PartialRatioStrategy>(() => new PartialRatioStrategy());

        public static PartialRatioStrategy Instance => s_lazy.Value;

        private PartialRatioStrategy() { }

        public override int Calculate(string input1, string input2)
        {
            string shorter;
            string longer;

            if (input1.Length < input2.Length)
            {
                shorter = input1;
                longer  = input2;
            }
            else
            {
                shorter = input2;
                longer  = input1;
            }

            MatchingBlock[] matchingBlocks = MatchingBlocksExtractor.Instance.GetMatchingBlocks(shorter, longer);

            List<double> scores = new List<double>();

            foreach (var matchingBlock in matchingBlocks)
            {
                int dist = matchingBlock.DestPos - matchingBlock.SourcePos;

                int longStart = dist > 0 ? dist : 0;
                int longEnd   = longStart + shorter.Length;

                if (longEnd > longer.Length) longEnd = longer.Length;

                string longSubstr = longer.Substring(longStart, longEnd - longStart);

                double ratio = Levenshtein.GetRatio(shorter, longSubstr);

                if (ratio > .995)
                {
                    return 100;
                }

                scores.Add(ratio);

            }

            return (int)Math.Round(100 * scores.Max());
        }
    }
}
