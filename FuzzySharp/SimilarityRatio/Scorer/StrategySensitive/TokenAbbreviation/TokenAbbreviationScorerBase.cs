using System;
using System.Collections.Generic;
using FuzzySharp.Extensions;
using FuzzySharp.Utils;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class TokenAbbreviationScorerBase : StrategySensitiveScorerBase
    {
        public override int Score(string input1, string input2)
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

            double lenRatio = (double)longer.Length / shorter.Length;

            // if longer isn't at least 1.5 times longer than the other, then its probably not an abbreviation
            if (lenRatio < 1.5) return 0;

            // numbers can't be abbreviations for other numbers, though that would be hilarious. "Yes, 4 - as in 4,238"
            var tokensLonger = longer.ExtractTokens();
            var tokensShorter = shorter.ExtractTokens();

            // more than 4 tokens and it's probably not an abbreviation (and could get costly)
            if (tokensShorter.Count > 4)
            {
                return 0;
            }

            List<string> moreTokens;
            List<string> fewerTokens;

            if (tokensLonger.Count > tokensShorter.Count)
            {
                moreTokens = tokensLonger;
                fewerTokens = tokensShorter;
            }
            else
            {
                moreTokens = tokensShorter;
                fewerTokens = tokensLonger;
            }

            var allPermutations = moreTokens.PermutationsOfSize(fewerTokens.Count);

            int maxScore = 0;

            foreach (var permutation in allPermutations)
            {
                double sum = 0;
                for (int i = 0; i < fewerTokens.Count; i++)
                {
                    var i1 = permutation[i];
                    var i2 = fewerTokens[i];
                    if (StringContainsInOrder(i1.AsSpan(), i2.AsSpan())) // must be at least twice as long
                    {
                        var score = Scorer(i1, i2);
                        sum += score;
                    }
                }
                var avgScore = (int) (sum / fewerTokens.Count);
                if(avgScore > maxScore)
                {
                    maxScore = avgScore;
                }
            }
            
            return maxScore;
        }

        /// <summary>
        /// Does s2 have all its characters appear in order in s1? (Basically, is s2 a potential abbreviation of s1?)
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        private static bool StringContainsInOrder(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
        {
            if (s1.Length < s2.Length) return false;
            int s2_idx = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s2[s2_idx] == s1[i])
                    s2_idx++;
                if (s2_idx == s2.Length)
                    return true;
                if (i + s2.Length - s2_idx == s1.Length)
                    return false;
            }
            return false;
        }
    }
}
