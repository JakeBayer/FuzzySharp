using System;
using System.Collections.Generic;

namespace FuzzySharp.Extractor
{
    public class ExtractedResult : IComparable<ExtractedResult> 
    {

        public string   Value;
        public int Score;
        public int Index;

        public ExtractedResult(string value, int score)
        {
            Value = value;
            Score = score;
        }

        public ExtractedResult(string value, int score, int index)
        {
            Value = value;
            Score = score;
            Index = index;
        }

        public int CompareTo(ExtractedResult other)
        {
            return Comparer<int>.Default.Compare(this.Score, other.Score);
        }

        public override string ToString()
        {
            return $"(string: {Value}, score: {Score}, index: {Index})";
        }
    }
}
