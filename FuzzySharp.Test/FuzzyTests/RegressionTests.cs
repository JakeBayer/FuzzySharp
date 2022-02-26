using FuzzySharp.SimilarityRatio.Scorer;
using NUnit.Framework;
using System;
using System.Linq;

namespace FuzzySharp.Test.FuzzyTests
{
    [TestFixture]
    public class RegressionTests
    {
        /// <summary>
        /// Test to ensure that all IRatioScorer implementations handle scoring empty strings & whitespace strings
        /// </summary>
        [Test]
        public void TestScoringEmptyString()
        {
            var scorerType = typeof(IRatioScorer);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var types = assemblies.SelectMany(s =>
            {
                Type[] types = new Type[] { }; ;
                try
                {
                    types = s.GetTypes();
                }
                catch { }
                return types;
            }).ToList();
            var scorerTypes = types.Where(t => scorerType.IsAssignableFrom(t) && !t.IsAbstract && t.IsClass).ToList();

            string emptyString = "";
            string whitespaceString = " ";

            string[] nullOrWhitespaceStrings = { emptyString, whitespaceString };

            foreach (Type t in scorerTypes)
            {
                System.Diagnostics.Debug.WriteLine($"Testing {t.Name}");
                var instance = Activator.CreateInstance(t, nonPublic: true);
                IRatioScorer scorer = (IRatioScorer)t.GetProperty("Instance").GetValue(instance, null);

                foreach (string s in nullOrWhitespaceStrings)
                {
                    System.Diagnostics.Debug.WriteLine($"Testing string '{s}'");
                    try
                    {
                        scorer.Score(s, "TEST");
                    }
                    catch (InvalidOperationException)
                    {
                        Assert.Fail($"{t.Name}.score failed with empty string as first parameter");
                    }
                    try
                    {
                        scorer.Score("TEST", s);
                    }
                    catch (InvalidOperationException)
                    {
                        Assert.Fail($"{t.Name}.score failed with empty string as second parameter");
                    }
                    try
                    {
                        scorer.Score(s, s);
                    }
                    catch (InvalidOperationException)
                    {
                        Assert.Fail($"{t.Name}.score failed with empty string as both parameters");
                    }
                }
            }
        }
    }
}