using FuzzySharp.Edits;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FuzzySharp
{
    internal interface ILevenshteinMatchingBlocks<T> where T : IEquatable<T>
    {
        IEnumerable<IMatchingBlock> GetMatchingBlocks(ReadOnlySpan<T> s1, ReadOnlySpan<T> s2);
    }

    internal class LevenshteinMatchingBlocks<T> : ILevenshteinMatchingBlocks<T> where T : IEquatable<T>
    {
        internal static readonly ILevenshteinMatchingBlocks<T> Instance = new LevenshteinMatchingBlocks<T>();

        public IEnumerable<IMatchingBlock> GetMatchingBlocks(ReadOnlySpan<T> s1, ReadOnlySpan<T> s2)
        {
            return GetMatchingBlocks(s1.Length, s2.Length, GetEditOps(s1, s2));
        }

        private static IReadOnlyList<IEditOp> EditOpsFromCostMatrix(
            int len1, ReadOnlySpan<T> c1, int p1, int o1,
            int len2, ReadOnlySpan<T> c2, int p2, int o2,
            int[] matrix)
        {
            int i, j, pos;

            int ptr;

            int dir = 0;

            pos = matrix[len1 * len2 - 1];

            var ops = new IEditOp[pos];

            i = len1 - 1;
            j = len2 - 1;

            ptr = len1 * len2 - 1;

            while (i > 0 || j > 0)
            {
                if (i != 0 && j != 0 && matrix[ptr] == matrix[ptr - len2 - 1]
                    && c1[p1 + i - 1].Equals(c2[p2 + j - 1]))
                {
                    i--;
                    j--;
                    ptr -= len2 + 1;
                    dir = 0;

                    continue;
                }

                if (dir < 0 && j != 0 && matrix[ptr] == matrix[ptr - 1] + 1)
                {
                    pos--;
                    ops[pos] = new EditOp(EditType.INSERT, i + o1, --j + o2);
                    ptr--;
                    continue;
                }

                if (dir > 0 && i != 0 && matrix[ptr] == matrix[ptr - len2] + 1)
                {
                    pos--;
                    ops[pos] = new EditOp(EditType.DELETE, --i + o1, j + o2);
                    ptr -= len2;
                    continue;
                }

                if (i != 0 && j != 0 && matrix[ptr] == matrix[ptr - len2 - 1] + 1)
                {
                    pos--;
                    ops[pos] = new EditOp(EditType.REPLACE, --i + o1, --j + o2);
                    ptr -= len2 + 1;
                    dir = 0;
                    continue;
                }

                if (dir == 0 && j != 0 && matrix[ptr] == matrix[ptr - 1] + 1)
                {
                    pos--;
                    ops[pos] = new EditOp(EditType.INSERT, i + o1, --j + o2);
                    ptr--;
                    dir = -1;
                    continue;
                }

                if (dir == 0 && i != 0 && matrix[ptr] == matrix[ptr - len2] + 1)
                {
                    pos--;
                    ops[pos] = new EditOp(EditType.DELETE, --i + o1, j + o2);
                    ptr -= len2;
                    dir = 1;
                    continue;
                }

                throw new InvalidOperationException("Cant calculate edit op");
            }

            return ops;
        }

        private static IReadOnlyList<IEditOp> GetEditOps(ReadOnlySpan<T> c1, ReadOnlySpan<T> c2)
        {
            var len1 = c1.Length;
            var len2 = c2.Length;
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

        private static IEnumerable<IMatchingBlock> GetMatchingBlocks(int len1, int len2, IReadOnlyList<IEditOp> ops)
        {
            int n = ops.Count;

            int i, SourcePos, DestPos;

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

            o = 0;
            SourcePos = DestPos = 0;

            for (i = n; i != 0;)
            {
                while (ops[o].EditType == EditType.KEEP && --i != 0)
                    o++;

                if (i == 0)
                    break;

                if (SourcePos < ops[o].SourcePos || DestPos < ops[o].DestPos)
                {
                    yield return new MatchingBlock(SourcePos, DestPos, ops[o].SourcePos - SourcePos);

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
                Debug.Assert(len1 - SourcePos == len2 - DestPos);

                yield return new MatchingBlock(SourcePos, DestPos, len1 - SourcePos);
            }

            // return final block
            yield return new MatchingBlock(len1, len2, 0);
        }
    }
}