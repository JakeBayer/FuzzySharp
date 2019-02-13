using System;
using System.Collections.Generic;
using System.Text;
using FuzzySharp.Edits;

namespace FuzzySharp.MatchingBlocks
{
    public interface IMatchingBlocksExtractor<in TBlock>
    {
        MatchingBlock[] GetMatchingBlocks(TBlock blocks1, TBlock blocks2);
    }
}
