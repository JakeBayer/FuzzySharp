using FuzzySharp.PreProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzySharp.Test.FuzzyTests
{
    [TestClass]
    public class RatioTests
    {
        #region Private Fields
        private string _s1,
                       _s1A,
                       _s2,
                       _s3,
                       _s4,
                       _s5,
                       _s6,
                       _s7,
                       _s8,
                       _s8A,
                       _s9,
                       _s9A,
                       _s10,
                       _s10A;

        private string[] _cirqueStrings, _baseballStrings;
        #endregion

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
            _s7  = "new york city mets - atlanta braves";
            // Edge cases
            _s8   = "{";
            _s8A  = "{";
            _s9   = "{a";
            _s9A  = "{a";
            _s10  = "a{";
            _s10A = "{b";
        }

        [TestMethod]
        public void Test_Equal()
        {
            Assert.AreEqual(Fuzz.Ratio(_s1, _s1A), 100);
            Assert.AreEqual(Fuzz.Ratio(_s8, _s8A), 100);
            Assert.AreEqual(Fuzz.Ratio(_s9, _s9A), 100);
        }

        [TestMethod]
        public void Test_Case_Insensitive()
        {
            Assert.AreNotEqual(Fuzz.Ratio(_s1, _s2), 100);
            Assert.AreEqual(Fuzz.Ratio(_s1, _s2, PreprocessMode.Full), 100);
        }

        [TestMethod]
        public void Test_Partial()
        {
            Assert.AreEqual(Fuzz.PartialRatio(_s1, _s3), 100);
        }

        [TestMethod]
        public void TestTokenSortRatio()
        {
            Assert.AreEqual(Fuzz.TokenSortRatio(_s1, _s1A), 100);
        }

        [TestMethod]
        public void TestPartialTokenSortRatio()
        {
            Assert.AreEqual(Fuzz.PartialTokenSortRatio(_s1, _s1A, PreprocessMode.Full), 100);
            Assert.AreEqual(Fuzz.PartialTokenSortRatio(_s4, _s5, PreprocessMode.Full), 100);
            Assert.AreEqual(Fuzz.PartialTokenSortRatio(_s8, _s8A), 100);
            Assert.AreEqual(Fuzz.PartialTokenSortRatio(_s9, _s9A, PreprocessMode.Full), 100);
            Assert.AreEqual(Fuzz.PartialTokenSortRatio(_s9, _s9A), 100);
            Assert.AreEqual(Fuzz.PartialTokenSortRatio(_s10, _s10A), 50);
        }

        [TestMethod]
        public void TestTokenSetRatio()
        {
            Assert.AreEqual(Fuzz.TokenSetRatio(_s4, _s5, PreprocessMode.Full), 100);
            Assert.AreEqual(Fuzz.TokenSetRatio(_s8, _s8A), 100);
            Assert.AreEqual(Fuzz.TokenSetRatio(_s9, _s9A, PreprocessMode.Full), 100);
            Assert.AreEqual(Fuzz.TokenSetRatio(_s9, _s9A), 100);
            Assert.AreEqual(Fuzz.TokenSetRatio(_s10, _s10A), 50);
        }

        [TestMethod]
        public void TestTokenAbbreviationRatio()
        {
            Assert.AreEqual(Fuzz.TokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full), 40);
            Assert.AreEqual(Fuzz.PartialTokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full), 50);
        }

        [TestMethod]
        public void TestPartialTokenSetRatio()
        {
            Assert.AreEqual(Fuzz.PartialTokenSetRatio(_s4, _s7), 100);
        }

        [TestMethod]
        public void TestWeightedRatioEqual()
        {
            Assert.AreEqual(Fuzz.WeightedRatio(_s1, _s1A), 100);
        }

        [TestMethod]
        public void TestWeightedRatioCaseInsensitive()
        {
            Assert.AreEqual(Fuzz.WeightedRatio(_s1, _s2, PreprocessMode.Full), 100);
        }

        [TestMethod]
        public void TestWeightedRatioPartialMatch()
        {
            Assert.AreEqual(Fuzz.WeightedRatio(_s1, _s3), 90);
        }

        [TestMethod]
        public void TestWeightedRatioMisorderedMatch()
        {
            Assert.AreEqual(Fuzz.WeightedRatio(_s4, _s5), 95);
        }

        [TestMethod]
        public void TestEmptyStringsScore100()
        {
            Assert.AreEqual(Fuzz.Ratio("", ""), 100);
            Assert.AreEqual(Fuzz.PartialRatio("", ""), 100);
        }

        [TestMethod]
        public void TestIssueSeven()
        {
            _s1 = "HSINCHUANG";
            _s2 = "SINJHUAN";
            _s3 = "LSINJHUANG DISTRIC";
            _s4 = "SINJHUANG DISTRICT";

            Assert.IsTrue(Fuzz.PartialRatio(_s1, _s2) > 75);
            Assert.IsTrue(Fuzz.PartialRatio(_s1, _s3) > 75);
            Assert.IsTrue(Fuzz.PartialRatio(_s1, _s4) > 75);
        }

        [TestMethod]
        public void TestRatioUnicodeString()
        {
            _s1 = "\u00C1";
            _s2 = "ABCD";
            var score = Fuzz.Ratio(_s1, _s2);
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void TestPartialRatioUnicodeString()
        {
            _s1 = "\u00C1";
            _s2 = "ABCD";
            var score = Fuzz.PartialRatio(_s1, _s2);
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void TestWRatioUnicodeString()
        {
            _s1 = "\u00C1";
            _s2 = "ABCD";
            var score = Fuzz.WeightedRatio(_s1, _s2);
            Assert.AreEqual(0, score);

            // Cyrillic.
            _s1   = "\u043f\u0441\u0438\u0445\u043e\u043b\u043e\u0433";
            _s2   = "\u043f\u0441\u0438\u0445\u043e\u0442\u0435\u0440\u0430\u043f\u0435\u0432\u0442";
            score = Fuzz.WeightedRatio(_s1, _s2);
            Assert.AreNotEqual(0, score);

            // Chinese.
            _s1   = "\u6211\u4e86\u89e3\u6570\u5b66";
            _s2   = "\u6211\u5b66\u6570\u5b66";
            score = Fuzz.WeightedRatio(_s1, _s2);
            Assert.AreNotEqual(0, score);
        }
    }
}
