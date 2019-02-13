using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FuzzySharp.Edits;

namespace FuzzySharp.Distance.Levenshtein
{
    public abstract class LevenshteinBase
    {
        public static MatchingBlock[] GetMatchingBlocks(int len1, int len2, OpCode[] ops)
        {

            int n = ops.Length;

            int noOfMB, i;
            int o = 0;

            noOfMB = 0;

            for (i = n; i-- != 0; o++)
            {
                if (ops[o].EditType == EditType.KEEP)
                {
                    noOfMB++;

                    while (i != 0 && ops[o].EditType == EditType.KEEP)
                    {
                        i--;
                        o++;
                    }

                    if (i == 0)
                        break;
                }
            }

            MatchingBlock[] matchingBlocks = new MatchingBlock[noOfMB + 1];
            int mb = 0;
            o = 0;
            matchingBlocks[mb] = new MatchingBlock();

            for (i = n; i != 0; i--, o++)
            {
                if (ops[o].EditType == EditType.KEEP)
                {
                    matchingBlocks[mb].SourcePos = ops[o].SourceBegin;
                    matchingBlocks[mb].DestPos = ops[o].DestBegin;

                    while (i != 0 && ops[o].EditType == EditType.KEEP)
                    {
                        i--;
                        o++;
                    }

                    if (i == 0)
                    {
                        matchingBlocks[mb].Length = len1 - matchingBlocks[mb].SourcePos;
                        mb++;
                        break;
                    }

                    matchingBlocks[mb].Length = ops[o].SourceBegin - matchingBlocks[mb].SourcePos;
                    mb++;
                    matchingBlocks[mb] = new MatchingBlock();
                }
            }

            Debug.Assert(mb != noOfMB);

            MatchingBlock finalBlock = new MatchingBlock
            {
                SourcePos = len1,
                DestPos = len2,
                Length = 0
            };

            matchingBlocks[mb] = finalBlock;

            return matchingBlocks;
        }


        private static OpCode[] EditOpsToOpCodes(EditOp[] ops, int len1, int len2)
        {
            int n = ops.Length;
            int noOfBlocks, i, SourcePos, DestPos;
            int o = 0;
            EditType type;

            noOfBlocks = 0;
            SourcePos = DestPos = 0;

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

                    noOfBlocks++;
                    SourcePos = ops[o].SourcePos;
                    DestPos = ops[o].DestPos;

                }

                // TODO: Is this right?
                noOfBlocks++;
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
                noOfBlocks++;

            OpCode[] opCodes = new OpCode[noOfBlocks];

            o = 0;
            SourcePos = DestPos = 0;
            int oIndex = 0;

            for (i = n; i != 0;)
            {

                while (ops[o].EditType == EditType.KEEP && --i != 0)
                    o++;

                if (i == 0)
                    break;

                OpCode oc = new OpCode();
                opCodes[oIndex] = oc;
                oc.SourceBegin = SourcePos;
                oc.DestBegin = DestPos;

                if (SourcePos < ops[o].SourcePos || DestPos < ops[o].DestPos)
                {

                    oc.EditType = EditType.KEEP;
                    SourcePos = oc.SourceEnd = ops[o].SourcePos;
                    DestPos = oc.DestEnd = ops[o].DestPos;

                    oIndex++;
                    OpCode oc2 = new OpCode();
                    opCodes[oIndex] = oc2;
                    oc2.SourceBegin = SourcePos;
                    oc2.DestBegin = DestPos;

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

                opCodes[oIndex].EditType = type;
                opCodes[oIndex].SourceEnd = SourcePos;
                opCodes[oIndex].DestEnd = DestPos;
                oIndex++;
            }

            if (SourcePos < len1 || DestPos < len2)
            {

                Debug.Assert(len1 - SourcePos == len2 - DestPos);
                if (opCodes[oIndex] == null)
                    opCodes[oIndex] = new OpCode();
                opCodes[oIndex].EditType = EditType.KEEP;
                opCodes[oIndex].SourceBegin = SourcePos;
                opCodes[oIndex].DestBegin = DestPos;
                opCodes[oIndex].SourceEnd = len1;
                opCodes[oIndex].DestEnd = len2;

                oIndex++;

            }

            Debug.Assert(oIndex == noOfBlocks);

            return opCodes;
        }

        protected static int EditDistance<T>(T[] c1, T[] c2, int xcost = 0) where T : IEquatable<T>
        {
            int i;
            int half;

            int str1 = 0;
            int str2 = 0;

            int len1 = c1.Length;
            int len2 = c2.Length;

            /* strip common prefix */
            while (len1 > 0 && len2 > 0 && c1[str1].Equals(c2[str2]))
            {

                len1--;
                len2--;
                str1++;
                str2++;

            }

            /* strip common suffix */
            while (len1 > 0 && len2 > 0 && c1[str1 + len1 - 1].Equals(c2[str2 + len2 - 1]))
            {
                len1--;
                len2--;
            }

            /* catch trivial cases */
            if (len1 == 0)
                return len2;
            if (len2 == 0)
                return len1;

            /* make the inner cycle (i.e. str2) the longer one */
            if (len1 > len2)
            {

                int nx   = len1;
                int temp = str1;

                len1 = len2;
                len2 = nx;

                str1 = str2;
                str2 = temp;

                T[] t = c2;
                c2 = c1;
                c1 = t;

            }

            /* check len1 == 1 separately */
            if (len1 == 1)
            {
                if (xcost != 0)
                {
                    return len2 + 1 - 2 * Memchr(c2, str2, c1[str1], len2);
                }
                else
                {
                    return len2 - Memchr(c2, str2, c1[str1], len2);
                }
            }

            len1++;
            len2++;
            half = len1 >> 1;

            int[] row = new int[len2];
            int   end = len2 - 1;

            for (i = 0; i < len2 - (xcost != 0 ? 0 : half); i++)
                row[i] = i;


            /* go through the matrix and compute the costs.  yes, this is an extremely
             * obfuscated version, but also extremely memory-conservative and relatively
             * fast.  */

            if (xcost != 0)
            {

                for (i = 1; i < len1; i++)
                {

                    int p = 1;

                    T   ch1 = c1[str1 + i - 1];
                    int c2p = str2;

                    int D = i;
                    int x = i;

                    while (p <= end)
                    {

                        if (ch1.Equals(c2[c2p++]))
                        {
                            x = --D;
                        }
                        else
                        {
                            x++;
                        }
                        D = row[p];
                        D++;

                        if (x > D)
                            x = D;
                        row[p++] = x;

                    }

                }

            }
            else
            {

                /* in this case we don't have to scan two corner triangles (of size len1/2)
                 * in the matrix because no best path can go throught them. note this
                 * breaks when len1 == len2 == 2 so the memchr() special case above is
                 * necessary */

                row[0] = len1 - half - 1;
                for (i = 1; i < len1; i++)
                {
                    int p;

                    T   ch1 = c1[str1 + i - 1];
                    int c2p;

                    int D, x;

                    /* skip the upper triangle */
                    if (i >= len1 - half)
                    {
                        int offset = i - (len1 - half);
                        int c3;

                        c2p = str2 + offset;
                        p   = offset;
                        c3  = row[p++] + (!ch1.Equals(c2[c2p++]) ? 1 : 0);
                        x   = row[p];
                        x++;
                        D = x;
                        if (x > c3)
                        {
                            x = c3;
                        }
                        row[p++] = x;
                    }
                    else
                    {
                        p   = 1;
                        c2p = str2;
                        D   = x = i;
                    }
                    /* skip the lower triangle */
                    if (i <= half + 1)
                        end = len2 + i - half - 2;
                    /* main */
                    while (p <= end)
                    {
                        int c3 = --D + (!ch1.Equals(c2[c2p++]) ? 1 : 0);
                        x++;
                        if (x > c3)
                        {
                            x = c3;
                        }
                        D = row[p];
                        D++;
                        if (x > D)
                            x = D;
                        row[p++] = x;

                    }

                    /* lower triangle sentinel */
                    if (i <= half)
                    {
                        int c3 = --D + (!ch1.Equals(c2[c2p]) ? 1 : 0);
                        x++;
                        if (x > c3)
                        {
                            x = c3;
                        }
                        row[p] = x;
                    }
                }
            }

            i = row[end];

            return i;
        }

        private static int Memchr<T>(IReadOnlyList<T> haystack, int offset, T needle, int num) where T : IEquatable<T>
        {

            if (num != 0)
            {
                int p = 0;

                do
                {
                    if (haystack[offset + p].Equals(needle))
                        return 1;

                    p++;
                } while (--num != 0);

            }
            return 0;
        }

        public static double GetRatio<T>(T[] input1, T[] input2) where T : IEquatable<T>
        {
            int len1   = input1.Length;
            int len2   = input2.Length;
            int lensum = len1 + len2;

            int editDistance = EditDistance(input1, input2, 1);

            return editDistance == 0 ? 1 : (lensum - editDistance) / (double)lensum;
        }
    }
}
