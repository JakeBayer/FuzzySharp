namespace FuzzySharp
{
    internal interface IEditOp
    {
        EditType EditType { get; }
        int SourcePos { get; }
        int DestPos { get; }
    }

    internal struct EditOp : IEditOp
    {
        internal EditOp(EditType editType, int sourcePosition, int destinationPosition)
        {
            EditType = editType;
            SourcePos = sourcePosition;
            DestPos = destinationPosition;
        }

        public readonly EditType EditType { get; }
        public readonly int SourcePos { get; }
        public readonly int DestPos { get; }

        public override string ToString()
        {
            return $"{EditType}({SourcePos}, {DestPos})";
        }
    }
}