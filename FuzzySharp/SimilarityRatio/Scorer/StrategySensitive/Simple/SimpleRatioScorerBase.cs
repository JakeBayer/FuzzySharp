namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class SimpleRatioScorerBase : StrategySensitiveScorerBase
    {
        public override int Score(string input1, string input2)
        {
            return Scorer(input1, input2);
        }
    }
}
