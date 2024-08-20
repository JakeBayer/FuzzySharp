using FuzzySharp.Extensions;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
    public abstract class TokenSortScorerBase : StrategySensitiveScorerBase
    {
        public override int Score(string input1, string input2)
        {
            var sorted1 = input1.NormalizeSpacesAndSort();
            var sorted2 = input2.NormalizeSpacesAndSort();

            return Scorer(sorted1, sorted2);
        }
    }
}
