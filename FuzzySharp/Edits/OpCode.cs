namespace FuzzySharp.Edits
{
    public class OpCode
    {
        public EditType EditType    { get; set; }
        public int      SourceBegin { get; set; }
        public int      SourceEnd   { get; set; }
        public int      DestBegin   { get; set; }
        public int      DestEnd     { get; set; }

        public override string ToString()
        {
            return $"{EditType}({SourceBegin},{SourceEnd},{DestBegin},{DestEnd})";
        }
    }
}
