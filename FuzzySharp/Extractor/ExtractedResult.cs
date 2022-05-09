using System;

namespace FuzzySharp.Extractor
{
    public struct ExtractedResult<T> : IComparable<ExtractedResult<T>>
    {
        public T Value { get; }
        public int Score { get; }
        public int Index { get; }

        public ExtractedResult(T value, int score) : this(value, score, 0)
        {
        }

        public ExtractedResult(T value, int score, int index)
        {
            Value = value;
            Score = score;
            Index = index;
        }

        public int CompareTo(ExtractedResult<T> other)
        {
            return Score.CompareTo(other.Score);
        }

        public override string ToString()
        {
            if (typeof(T) == typeof(string))
            {
                return $"(string: {Value}, score: {Score}, index: {Index})";
            }
            return $"(value: {Value.ToString()}, score: {Score}, index: {Index})";
        }
    }
}
