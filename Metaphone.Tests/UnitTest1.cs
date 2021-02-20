using NUnit.Framework;
using System.Linq;

namespace Metaphone.Tests
{
    public class Tests
    {
        public Metaphone TestMetaphone;

        [SetUp]
        public void Setup()
        {
            TestMetaphone = new Metaphone();
        }

        [TestCase("WHERE")]
        public void Should_ReturnTrueIf_MatchWithWH(string word)
        {
            var res = TestMetaphone.Match(word, 0, Metaphone.ChangeInitialWH);
            Assert.True(res);
        }

        [TestCase("WILLYWHONKA")]
        public void Should_ReturnFalseIf_NotMatchWithWH(string word)
        {
            var res = TestMetaphone.Match(word, 0, Metaphone.ChangeInitialWH);
            Assert.False(res);
        }

        [TestCase("ABRAKADABRA")]
        [TestCase("OJSAN")]
        public void Should_ReturnTrueIf_MatchWithVowel(string word)
        {
            var res = TestMetaphone.IsVowel(word, 0);
            Assert.True(res);
        }

        [TestCase("JOHODU")]
        public void Should_ReturnFalseIf_NotMatchWithVowel(string word)
        {
            var res = TestMetaphone.IsVowel(word, 0);
            Assert.False(res);
        }

        [TestCase("OHODU")]
        public void Should_ReturnTrueIf_InitialCharacterIsVowel(string word)
        {
            var res = TestMetaphone.TransInitial(word);
            Assert.True(Metaphone.Vowels.Any(c => c == res));
        }

        [TestCase("WHODU")]
        public void Should_ReturnTrueIf_ResultIsW(string word)
        {
            var res = TestMetaphone.TransInitial(word);
            Assert.True(res == 'W');
        }

        [TestCase("XAVIER")]
        public void Should_ReturnTrueIf_ResultIsS(string word)
        {
            var res = TestMetaphone.TransInitial(word);
            Assert.True(res == 'S');
        }

        [TestCase("KNIGHT")]
        [TestCase("ACCOUNT")]
        public void Should_ReturnTrueIf_ResultIsSecondLetterInWord(string word)
        {
            var res = TestMetaphone.TransInitial(word);
            Assert.True(res == word[1]);
        }

        [TestCase("FISK")]
        public void Should_ReturnTrueIf_ResultIsCharMin(string word)
        {
            var res = TestMetaphone.TransInitial(word);
            Assert.True(res == char.MinValue);
        }
    }
}