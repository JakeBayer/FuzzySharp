using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Scorer;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzySharp.Test.FuzzyTests.ScorerTests
{
    [TestFixture]
    public class TokenSetScorerBaseTest
    {
        private const string fuzzy1 = "Fuzzy Wuzzy Was A Bear";
        private const string fuzzy2 = "Fuzzy Fuzzy Fuzzy Bear";

        private const string nasa1 = "National Aeronautics Space Administration";
        private const string nasa2 = "Space Museum, Cape Canaveral";
        private const string nersa = "Nertional Aronutics Spooce Adminostration";

        private IRatioScorer _scorer;
        private IRatioScorer _partialScorer;

        [SetUp]
        public void SetUp()
        {
            _scorer = ScorerCache.Get<TokenSetScorer>();
            _partialScorer = ScorerCache.Get<PartialTokenSetScorer>();
        }

        [TestCase(fuzzy1, fuzzy1)]
        [TestCase(fuzzy2, fuzzy2)]
        [TestCase(nasa1, nasa1)]
        [TestCase(nasa2, nasa2)]
        public void TokenSetScorer_SameInput_Returns100(string s1, string s2)
        {
            Assert.That(_scorer.Score(s1, s2), Is.EqualTo(100));
            Assert.That(_partialScorer.Score(s1, s2), Is.EqualTo(100));
        }

        [Test]
        public void TokenSetScorer_OneSetContainsAllTokensOfTheOther_Returns100()
        {
            Assert.That(_scorer.Score(fuzzy1, fuzzy2), Is.EqualTo(100));
            Assert.That(_partialScorer.Score(fuzzy1, fuzzy2), Is.EqualTo(100));
        }

        [Test]
        public void TokenSetScorer_WhenOnlySingleTokenSimilar_DoesNotReturn100()
        {
            Assert.That(_scorer.Score(nasa1, nasa2), Is.Not.EqualTo(100));
        }

        [Test]
        public void PartialTokenSetScorer_WhenOnlySingleTokenSimilar_StillReturns100()
        {
            Assert.That(_partialScorer.Score(nasa1, nasa2), Is.EqualTo(100));
        }

        [Test]
        public void PartialTokenSetScorer_WhenNoTokenSimilar_DoesNotReturn100()
        {
            Assert.That(_partialScorer.Score(nasa1, nersa), Is.Not.EqualTo(100));
        }
    }
}
