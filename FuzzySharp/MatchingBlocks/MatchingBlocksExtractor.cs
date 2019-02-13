using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.Edits;

namespace FuzzySharp.MatchingBlocks
{
    public class MatchingBlocksExtractor : MatchingBlocksExtractorBase, IMatchingBlocksExtractor<string>
    {
        private static readonly Lazy<MatchingBlocksExtractor> s_lazy = new Lazy<MatchingBlocksExtractor>(() => new MatchingBlocksExtractor());

        public static MatchingBlocksExtractor Instance => s_lazy.Value;

        private MatchingBlocksExtractor() { }

        public MatchingBlock[] GetMatchingBlocks(string s1, string s2)
        {
            return GetMatchingBlocks(s1.Length, s2.Length, GetEditOps(s1, s2));
        }

        private static EditOp[] GetEditOps(string s1, string s2)
        {
            return GetEditOps(s1.Length, s1.ToCharArray(), s2.Length, s2.ToCharArray());
        }
    }

    public class MatchingBlocksExtractor<TBlock> : MatchingBlocksExtractorBase, IMatchingBlocksExtractor<TBlock[]> 
        where TBlock : IEquatable<TBlock>
    {
        private static readonly Lazy<MatchingBlocksExtractor<TBlock>> s_lazy = new Lazy<MatchingBlocksExtractor<TBlock>>(() => new MatchingBlocksExtractor<TBlock>());

        public static MatchingBlocksExtractor<TBlock> Instance => s_lazy.Value;

        private MatchingBlocksExtractor() { }

        public MatchingBlock[] GetMatchingBlocks(TBlock[] blocks1, TBlock[] blocks2)
        {
            return GetMatchingBlocks(blocks1.Length, blocks2.Length, GetEditOps(blocks1.Length, blocks1, blocks2.Length, blocks2));
        }

        private static EditOp[] GetEditOps(TBlock[] arr1, TBlock[] arr2)
        {
            return GetEditOps(arr1.Length, arr1, arr2.Length, arr2);
        }
    }
}
