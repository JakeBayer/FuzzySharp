using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FuzzySharp.Edits;

namespace FuzzySharp.MatchingBlocks
{
    public abstract class MatchingBlocksExtractorBase
    {
        #region Get Matching Blocks
        protected static MatchingBlock[] GetMatchingBlocks(int len1, int len2, EditOp[] ops)
        {

            int n = ops.Length;

            int numberOfMatchingBlocks, i, SourcePos, DestPos;

            numberOfMatchingBlocks = 0;

            int o = 0;

            SourcePos = DestPos = 0;

            EditType type;

            for (i = n; i != 0;)
            {


                while (ops[o].EditType == EditType.KEEP && --i != 0)
                {
                    o++;
                }

                if (i == 0)
                    break;

                if (SourcePos < ops[o].SourcePos || DestPos < ops[o].DestPos)
                {

                    numberOfMatchingBlocks++;
                    SourcePos = ops[o].SourcePos;
                    DestPos = ops[o].DestPos;

                }

                type = ops[o].EditType;

                switch (type)
                {
                    case EditType.REPLACE:
                        do
                        {
                            SourcePos++;
                            DestPos++;
                            i--;
                            o++;
                        } while (i != 0 && ops[o].EditType == type &&
                                SourcePos == ops[o].SourcePos && DestPos == ops[o].DestPos);
                        break;

                    case EditType.DELETE:
                        do
                        {
                            SourcePos++;
                            i--;
                            o++;
                        } while (i != 0 && ops[o].EditType == type &&
                                SourcePos == ops[o].SourcePos && DestPos == ops[o].DestPos);
                        break;

                    case EditType.INSERT:
                        do
                        {
                            DestPos++;
                            i--;
                            o++;
                        } while (i != 0 && ops[o].EditType == type &&
                                SourcePos == ops[o].SourcePos && DestPos == ops[o].DestPos);
                        break;

                    default:
                        break;
                }
            }

            if (SourcePos < len1 || DestPos < len2)
            {
                numberOfMatchingBlocks++;
            }

            MatchingBlock[] matchingBlocks = new MatchingBlock[numberOfMatchingBlocks + 1];

            o = 0;
            SourcePos = DestPos = 0;
            int mbIndex = 0;


            for (i = n; i != 0;)
            {

                while (ops[o].EditType == EditType.KEEP && --i != 0)
                    o++;

                if (i == 0)
                    break;

                if (SourcePos < ops[o].SourcePos || DestPos < ops[o].DestPos)
                {
                    MatchingBlock mb = new MatchingBlock();

                    mb.SourcePos = SourcePos;
                    mb.DestPos = DestPos;
                    mb.Length = ops[o].SourcePos - SourcePos;
                    SourcePos = ops[o].SourcePos;
                    DestPos = ops[o].DestPos;

                    matchingBlocks[mbIndex++] = mb;

                }

                type = ops[o].EditType;

                switch (type)
                {
                    case EditType.REPLACE:
                        do
                        {
                            SourcePos++;
                            DestPos++;
                            i--;
                            o++;
                        } while (i != 0 && ops[o].EditType == type &&
                                SourcePos == ops[o].SourcePos && DestPos == ops[o].DestPos);
                        break;

                    case EditType.DELETE:
                        do
                        {
                            SourcePos++;
                            i--;
                            o++;
                        } while (i != 0 && ops[o].EditType == type &&
                                SourcePos == ops[o].SourcePos && DestPos == ops[o].DestPos);
                        break;

                    case EditType.INSERT:
                        do
                        {
                            DestPos++;
                            i--;
                            o++;
                        } while (i != 0 && ops[o].EditType == type &&
                                SourcePos == ops[o].SourcePos && DestPos == ops[o].DestPos);
                        break;

                    default:
                        break;
                }
            }

            if (SourcePos < len1 || DestPos < len2)
            {
                Debug.Assert(len1 - SourcePos == len2 - DestPos);

                MatchingBlock mb = new MatchingBlock();
                mb.SourcePos = SourcePos;
                mb.DestPos = DestPos;
                mb.Length = len1 - SourcePos;

                matchingBlocks[mbIndex++] = mb;
            }

            Debug.Assert(numberOfMatchingBlocks == mbIndex);

            MatchingBlock finalBlock = new MatchingBlock();
            finalBlock.SourcePos = len1;
            finalBlock.DestPos = len2;
            finalBlock.Length = 0;

            matchingBlocks[mbIndex] = finalBlock;

            return matchingBlocks;
        }
        #endregion

        #region Get Edit Operations
        protected static EditOp[] GetEditOps<T>(int len1, T[] c1, int len2, T[] c2) where T : IEquatable<T>
        {
            int i;

            int[] matrix;

            int p1 = 0;
            int p2 = 0;

            var len1o = 0;

            while (len1 > 0 && len2 > 0 && c1[p1].Equals(c2[p2]))
            {
                len1--;
                len2--;

                p1++;
                p2++;

                len1o++;
            }

            var len2o = len1o;

            /* strip common suffix */
            while (len1 > 0 && len2 > 0 && c1[p1 + len1 - 1].Equals(c2[p2 + len2 - 1]))
            {
                len1--;
                len2--;
            }

            len1++;
            len2++;

            matrix = new int[len2 * len1];

            for (i = 0; i < len2; i++)
                matrix[i] = i;
            for (i = 1; i < len1; i++)
                matrix[len2 * i] = i;

            for (i = 1; i < len1; i++)
            {

                int ptrPrev = (i - 1) * len2;
                int ptrC = i * len2;
                int ptrEnd = ptrC + len2 - 1;

                T char1 = c1[p1 + i - 1];
                int ptrChar2 = p2;

                int x = i;

                ptrC++;

                while (ptrC <= ptrEnd)
                {

                    int c3 = matrix[ptrPrev++] + (!char1.Equals(c2[ptrChar2++]) ? 1 : 0);
                    x++;

                    if (x > c3)
                    {
                        x = c3;
                    }

                    c3 = matrix[ptrPrev] + 1;

                    if (x > c3)
                    {
                        x = c3;
                    }

                    matrix[ptrC++] = x;

                }

            }


            return EditOpsFromCostMatrix(len1, c1, p1, len1o, len2, c2, p2, len2o, matrix);
        }


        private static EditOp[] EditOpsFromCostMatrix<T>(int len1, T[] c1, int p1, int o1,
                                                      int len2, T[] c2, int p2, int o2,
                                                      int[] matrix)
            where T : IEquatable<T>
        {

            int i, j, pos;

            int ptr;

            EditOp[] ops;

            int dir = 0;

            pos = matrix[len1 * len2 - 1];

            ops = new EditOp[pos];

            i = len1 - 1;
            j = len2 - 1;

            ptr = len1 * len2 - 1;

            while (i > 0 || j > 0)
            {

                if (dir < 0 && j != 0 && matrix[ptr] == matrix[ptr - 1] + 1)
                {

                    EditOp eop = new EditOp();

                    pos--;
                    ops[pos] = eop;
                    eop.EditType = EditType.INSERT;
                    eop.SourcePos = i + o1;
                    eop.DestPos = --j + o2;
                    ptr--;

                    continue;
                }

                if (dir > 0 && i != 0 && matrix[ptr] == matrix[ptr - len2] + 1)
                {

                    EditOp eop = new EditOp();

                    pos--;
                    ops[pos] = eop;
                    eop.EditType = EditType.DELETE;
                    eop.SourcePos = --i + o1;
                    eop.DestPos = j + o2;
                    ptr -= len2;

                    continue;

                }

                if (i != 0 && j != 0 && matrix[ptr] == matrix[ptr - len2 - 1]
                        && c1[p1 + i - 1].Equals(c2[p2 + j - 1]))
                {

                    i--;
                    j--;
                    ptr -= len2 + 1;
                    dir = 0;

                    continue;

                }

                if (i != 0 && j != 0 && matrix[ptr] == matrix[ptr - len2 - 1] + 1)
                {

                    pos--;

                    EditOp eop = new EditOp();
                    ops[pos] = eop;

                    eop.EditType = EditType.REPLACE;
                    eop.SourcePos = --i + o1;
                    eop.DestPos = --j + o2;

                    ptr -= len2 + 1;
                    dir = 0;
                    continue;

                }

                if (dir == 0 && j != 0 && matrix[ptr] == matrix[ptr - 1] + 1)
                {

                    pos--;
                    EditOp eop = new EditOp();
                    ops[pos] = eop;
                    eop.EditType = EditType.INSERT;
                    eop.SourcePos = i + o1;
                    eop.DestPos = --j + o2;
                    ptr--;
                    dir = -1;

                    continue;
                }

                if (dir == 0 && i != 0 && matrix[ptr] == matrix[ptr - len2] + 1)
                {
                    pos--;
                    EditOp eop = new EditOp();
                    ops[pos] = eop;

                    eop.EditType = EditType.DELETE;
                    eop.SourcePos = --i + o1;
                    eop.DestPos = j + o2;
                    ptr -= len2;
                    dir = 1;
                    continue;
                }

                throw new InvalidOperationException("Cant calculate edit op");
            }

            return ops;
        }
        #endregion
    }
}
