using taxTelecomTasks.Core;

namespace taxTelecomTasks.Tests
{
    public class Tests
    {
        // Тест задания 1
        [TestCase("ads", "sdaads")]
        [TestCase("abcdef", "cbafed")]
        [TestCase("abcde", "edcbaabcde")]
        public void ReverseStringTest(string input, string expectOut)
        {
            var testOut = StringProcessor.ReverseString(input);

            Assert.That(testOut, Is.EqualTo(expectOut));
        }


        // Тест задания 2
        [TestCase("Abcde","A", false)]
        [TestCase("bcde","", true)]
        [TestCase("refErenC","EC", false)]
        [TestCase("","", true)]
        public void IsAllLowerTest(string input, string expectOut, bool isValid)
        {
            var testOut = StringProcessor.IsAllLower(input, out var wrongChars);

            Assert.That(testOut, Is.EqualTo(isValid));
            Assert.That(wrongChars, Is.EqualTo(expectOut));

        }

        //// Тест задание 3
        static IEnumerable<object[]> firstMethod()
        {
            var dictionary = new Dictionary<char, int>()
            {
                {'d', 2},
                {'a', 2},
                {'s', 1},
                {'f', 1},
                {'e', 1},
                {'r', 1}
            };
            return new[] { new object[] { "daasdfer", dictionary}, };
        }
        static IEnumerable<object[]> secondMethod()
        {
            var dictionary = new Dictionary<char, int>()
            {
                {'f', 2},
                {'e', 1},
                {'r', 2},
                {'q', 1}
            };
            return new[] { new object[] { "ferfrq", dictionary}, };
        }
        static IEnumerable<object[]> thirdMethod()
        {
            var dictionary = new Dictionary<char, int>()
            {
                {'d', 1},
                {'r', 2},
                {'o', 2},
                {'w', 1},
                {'l', 2}
            };
            return new[] { new object[] { "drowllor", dictionary}, };
        }

        [TestCaseSource(nameof(firstMethod))]
        [TestCaseSource(nameof(secondMethod))]
        [TestCaseSource(nameof(thirdMethod))]
        public void GetLetterMatchesTest(string input, Dictionary<char, int> exp)
        {
            var testOut = StringProcessor.GetLetterMatches(input);
            
            Assert.That(testOut, Is.EqualTo(exp));
        }

        // Тест задания 4

        [TestCase("caferrefac", "aferrefa")]
        [TestCase("sdorwymmywrods", "orwymmywro")]
        [TestCase("tseuqerrequest", "euqerreque")]
        [TestCase("etxatmocel", "etxatmoce")]
        [TestCase("rtrimyamdu", "imyamdu")]
        [TestCase("ettsettsetts", "ettsettse")]
        [TestCase("llordrow", "ordro")]
        [TestCase("ra", "a")]
        [TestCase("tete", "ete")]
        public void GetLongestSubStringTest(string input, string expectOutput)
        {
            var methodOutput = StringProcessor.GetLongestSubString(input);

            Assert.That(methodOutput, Is.EqualTo(expectOutput));
        }

        // Тест задания 5

        [TestCase("hserferrefresh", "eeeeffhhrrrrss")]
        [TestCase("nirtstsetg", "eginrssttt")]
        [TestCase("ss", "ss")]
        [TestCase("ed", "de")]
        [TestCase("gfedcbaabcdefg", "aabbccddeeffgg")]
        [TestCase("trostros", "oorrsstt")]
        [TestCase("satxtk", "aksttx")]
        [TestCase("eriuqerrequire", "eeeeiiqqrrrruu")]
        [TestCase("ruknag", "agknru")]
        [TestCase("gnitsetenoddonetesting", "ddeeeeggiinnnnoosstttt")]
        public void SortStringTest(string input, string expectedOutput)
        {
            var quickOutput = QuickSort.SortString(input);
            var treeOutput = TreeSort.SortString(input);

            Assert.That(quickOutput, Is.EqualTo(expectedOutput));
            Assert.That(treeOutput, Is.EqualTo(expectedOutput));
        }

    }
}