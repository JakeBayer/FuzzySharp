using System.Collections.Generic;
using System.Linq;
using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzySharp.Test.FuzzyTests
{
    [TestClass]
    public class ProcessTests
    {
        private string   _s1;
        private string   _s1A;
        private string   _s2;
        private string   _s3;
        private string   _s4;
        private string   _s5;
        private string   _s6;
        private string[] _cirqueStrings;
        private string[] _baseballStrings;

        [TestInitialize]
        public void Setup()
        {
            _s1  = "new york mets";
            _s1A = "new york mets";
            _s2  = "new YORK mets";
            _s3  = "the wonderful new york mets";
            _s4  = "new york mets vs atlanta braves";
            _s5  = "atlanta braves vs new york mets";
            _s6  = "new york mets - atlanta braves";
            _cirqueStrings = new[]
            {
                "cirque du soleil - zarkana - las vegas",
                "cirque du soleil ",
                "cirque du soleil las vegas",
                "zarkana las vegas",
                "las vegas cirque du soleil at the bellagio",
                "zarakana - cirque du soleil - bellagio"
            };

            _baseballStrings = new[]
            {
                "new york mets vs chicago cubs",
                "chicago cubs vs chicago white sox",
                "philladelphia phillies vs atlanta braves",
                "braves vs mets",
            };
        }

        [TestMethod]
        public void TestGetBestChoice1()
        {
            var query = "new york mets at atlanta braves";
            var best  = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, "braves vs mets");

        }

        [TestMethod]
        public void TestGetBestChoice2()
        {
            var query = "philadelphia phillies at atlanta braves";
            var best  = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, _baseballStrings[2]);

        }

        [TestMethod]
        public void TestGetBestChoice3()
        {
            var query = "atlanta braves at philadelphia phillies";
            var best  = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, _baseballStrings[2]);

        }

        [TestMethod]
        public void TestGetBestChoice4()
        {
            var query = "chicago cubs vs new york mets";
            var best  = Process.ExtractOne(query, _baseballStrings);
            Assert.AreEqual(best.Value, _baseballStrings[0]);

        }

        [TestMethod]
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

        [TestMethod]
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

            best = Process.ExtractOne(query, choices, null, ScorerCache.Get<DefaultRatioScorer>());
            Assert.AreEqual(best.Value, choices[0]);

            best = Process.ExtractOne(query, choicesDict.Select(k => k.Value));
            Assert.AreEqual(best.Value, choicesDict[1]);

        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

//[TestMethod]
//public void  generate_choices() {
//            choices = ['a', 'Bb', 'CcC']
//            for choice in choices {
//                yield choice
//        search = 'aaa'
//        result = [(value, confidence) for value, confidence in
//                  Process.Extract(search, generate_choices())]
//        .assertTrue(len(result) > 0)

//    }

//[TestMethod]
//public void  test_dict_like_Extract() {
//        """We should be able to use a dict-like object for choices, not only a
//        dict, and still get dict-like output.
//        """
//        try {
//            from UserDict import UserDict
//        except ImportError {
//            from collections import UserDict
//        choices = UserDict({ 'aa' { 'bb', 'a1' { None})
//        search = 'aaa'
//        result = Process.Extract(search, choices)
//        .assertTrue(len(result) > 0)
//        for value, confidence, key in result {
//            .assertTrue(value in choices.values())

//    }

//[TestMethod]
//public void  test_dedupe() {
//        """We should be able to use a list-like object for contains_dupes
//        """
//        // Test 1
//        contains_dupes = ['Frodo Baggins', 'Tom Sawyer', 'Bilbo Baggin', 'Samuel L. Jackson', 'F. Baggins', 'Frody Baggins', 'Bilbo Baggins']

//        result = Process.dedupe(contains_dupes)
//        .assertTrue(len(result) < len(contains_dupes))

//        // Test 2
//        contains_dupes = ['Tom', 'Dick', 'Harry']

//// we should end up with the same list since no duplicates are contained in the list (e.g. original list is returned)
//        deduped_list = ['Tom', 'Dick', 'Harry']

//        result = Process.dedupe(contains_dupes)
//        Assert.AreEqual(result, deduped_list)

//    }

//[TestMethod]
//public void  test_simplematch() {
//        basic_string = 'a, b'
//        match_strings = ['a, b']

//        result = Process.ExtractOne(basic_string, match_strings, scorer=fuzz.ratio)
//        part_result = Process.ExtractOne(basic_string, match_strings, scorer=fuzz.partial_ratio)

//        Assert.AreEqual(result, ('a, b', 100))
//        Assert.AreEqual(part_result, ('a, b', 100))

//    }
    }
}
