using System.Collections.Generic;
using System.Linq;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive;
using NUnit.Framework;

namespace FuzzySharp.Test.FuzzyTests
{
    [TestFixture]
    public class ProcessTests
    {
        private string[] _baseballStrings;

        [SetUp]
        public void Setup()
        {
            _baseballStrings = new[]
            {
                "new york mets vs chicago cubs",
                "chicago cubs vs chicago white sox",
                "philladelphia phillies vs atlanta braves",
                "braves vs mets",
            };
        }

        [Test]
        public void TestGetBestChoice1()
        {
            var query = "new york mets at atlanta braves";
            var best = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, "braves vs mets");

        }

        [Test]
        public void TestGetBestChoice2()
        {
            var query = "philadelphia phillies at atlanta braves";
            var best = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, _baseballStrings[2]);

        }

        [Test]
        public void TestGetBestChoice3()
        {
            var query = "atlanta braves at philadelphia phillies";
            var best = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, _baseballStrings[2]);

        }

        [Test]
        public void TestGetBestChoice4()
        {
            var query = "chicago cubs vs new york mets";
            var best = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, _baseballStrings[0]);

        }

        [Test]
        public void TestWithProcessor()
        {
            var events = new[]
            {
                new[] { "chicago cubs vs new york mets", "CitiField", "2011-05-11", "8pm" },
                new[] { "new york yankees vs boston red sox", "Fenway Park", "2011-05-11", "8pm" },
                new[] { "atlanta braves vs pittsburgh pirates", "PNC Park", "2011-05-11", "8pm" },
            };
            var query = new[] { "new york mets vs chicago cubs", "CitiField", "2017-03-19", "8pm" };

            var best = Process.ExtractOne(query, events, strings => strings[0]);
            Assert.AreEqual(best.Value, events[0]);
        }

        [Test]
        public void TestWithScorer()
        {
            var choices = new[]
            {
                "new york mets vs chicago cubs",
                "chicago cubs at new york mets",
                "atlanta braves vs pittsbugh pirates",
                "new york yankees vs boston red sox"
            };

            var choicesDict = new Dictionary<int, string>
            {
                [1] = "new york mets vs chicago cubs",
                [2] = "chicago cubs vs chicago white sox",
                [3] = "philladelphia phillies vs atlanta braves",
                [4] = "braves vs mets"
            };

            // in this hypothetical example we care about ordering, so we use quick ratio
            var query = "new york mets at chicago cubs";

            // first, as an example, the normal way would select the "more
            // 'complete' match of choices[1]"

            var best = Process.ExtractOne(query, choices);
            Assert.AreEqual(best.Value, choices[1]);

            // now, use the custom scorer

            best = Process.ExtractOne(query, choices, null, DefaultRatioScorer.Instance);
            Assert.AreEqual(best.Value, choices[0]);

            best = Process.ExtractOne(query, choicesDict.Select(k => k.Value));
            Assert.AreEqual(best.Value, choicesDict[1]);

        }

        [Test]
        public void TestWithCutoff()
        {
            var choices = new[]
            {
                "new york mets vs chicago cubs",
                "chicago cubs at new york mets",
                "atlanta braves vs pittsbugh pirates",
                "new york yankees vs boston red sox"
            };

            var query = "los angeles dodgers vs san francisco giants";

            // in this situation, this is an event that does not exist in the list
            // we don't want to randomly match to something, so we use a reasonable cutoff

            var best = Process.ExtractSorted(query, choices, cutoff: 50);
            Assert.IsTrue(!best.Any());
            // .assertIsNone(best) // unittest.TestCase did not have assertIsNone until Python 2.7

            // however if we had no cutoff, something would get returned

            // best = Process.ExtractOne(query, choices)
            // .assertIsNotNone(best)

        }

        [Test]
        public void TestWithCutoff2()
        {
            var choices = new[]
            {
                "new york mets vs chicago cubs",
                "chicago cubs at new york mets",
                "atlanta braves vs pittsbugh pirates",
                "new york yankees vs boston red sox"
            };

            var query = "new york mets vs chicago cubs";
            // Only find 100-score cases
            var res = Process.ExtractSorted(query, choices, cutoff: 100);
            Assert.IsTrue(res.Any());
            var bestMatch = res.First();
            Assert.IsTrue(bestMatch.Value == choices[0]);

        }

        [Test]
        public void TestEmptyStrings()
        {
            var choices = new[]
            {
                "",
                "new york mets vs chicago cubs",
                "new york yankees vs boston red sox",
                "",
                ""
            };

            var query = "new york mets at chicago cubs";

            var best = Process.ExtractOne(query, choices);
            Assert.AreEqual(best.Value, choices[1]);
        }
    }
}
