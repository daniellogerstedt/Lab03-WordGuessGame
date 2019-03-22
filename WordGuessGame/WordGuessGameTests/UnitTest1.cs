using System;
using Xunit;
using WordGuessGame;

namespace WordGuessGameTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("TestOne")]
        [InlineData("TestTwo")]
        [InlineData("TestThree")]
        public void TestAddWord(string word)
        {
            string path = "./testAddWord.txt";
            System.IO.File.WriteAllLines(path, new string[0]);
            string[] testArrayPreAdd = System.IO.File.ReadAllLines(path);
            Assert.Empty(testArrayPreAdd);
            Program.PostWord(path, word);
            string[] testArrayPostAdd = System.IO.File.ReadAllLines(path);
            Assert.Equal(testArrayPostAdd[0], word);
        }

        [Theory]
        [InlineData("TestOne")]
        [InlineData("TestTwo")]
        [InlineData("TestThree")]
        public void TestDeleteWord(string word)
        {
            string path = "./testDeleteWord.txt";
            System.IO.File.WriteAllLines(path, new string[]{ word });
            string[] testArrayPreAdd = System.IO.File.ReadAllLines(path);
            Assert.Equal(testArrayPreAdd[0], word);
            Program.DeleteWord(path, word);
            string[] testArrayPostAdd = System.IO.File.ReadAllLines(path);
            Assert.Empty(testArrayPostAdd);
        }


        [Theory]
        [InlineData('a', false)]
        [InlineData('d', true)]
        [InlineData('o', true)]
        [InlineData('g', true)]
        [InlineData('s', false)]
        public void TestCheckLetter(char letter, bool exists)
        {
            bool testedLetter = Program.CheckLetter(new char[] { letter }, new char[] { 'd', 'o', 'g' }, new char[] { '_', '_', '_' });
            Assert.Equal(testedLetter, exists);
        }

    }
}
