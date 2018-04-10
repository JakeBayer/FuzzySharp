namespace FuzzySharp.Edits
{
    public enum EditType
    {
        DELETE = 0,
        EQUAL = 1,
        INSERT = 2,
        REPLACE = 3,
        KEEP = 4,
    }

    public class EditOp
    {
        public EditType EditType { get; set; }
        public int SourcePos { get; set; }
        public int DestPos { get; set; }

        public override string ToString()
        {
            return $"{EditType}({SourcePos}, {DestPos})";
        }
    }
}
