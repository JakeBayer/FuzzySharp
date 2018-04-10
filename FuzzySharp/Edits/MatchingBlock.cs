namespace FuzzySharp.Edits
{
    public class MatchingBlock
    {
        public int SourcePos { get; set; }
        public int DestPos { get; set; }
        public int Length { get; set; }

        public override string ToString()
        {
            return $"({SourcePos},{DestPos},{Length})";
        }
    }
}
