namespace FuzzySharp.Edits
{
    internal interface IMatchingBlock
    {
        int SourcePos { get; }
        int DestPos { get; }
        int Length { get; }
    }
    internal struct MatchingBlock : IMatchingBlock
    {
        internal MatchingBlock(int sourcePosition, int destinationPosition, int length)
        {
            SourcePos = sourcePosition;
            DestPos = destinationPosition;
            Length = length;
        }
        public readonly int SourcePos { get; }
        public readonly int DestPos { get; }
        public readonly int Length { get; }

        public override string ToString()
        {
            return $"({SourcePos},{DestPos},{Length})";
        }
    }
}
