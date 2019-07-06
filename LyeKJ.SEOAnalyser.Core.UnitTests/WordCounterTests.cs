using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace LyeKJ.SEOAnalyser.Core.UnitTests
{
    public class WordCounterTests
    {
        private readonly WordCounter wordCounter;

        public WordCounterTests()
        {
            wordCounter = new WordCounter();
        }
       
        [Fact]
        public void GetWordCountMap_ListOfWords()
        {
            var words = new List<string> { "Apple", "orange", "apple", "oranges" };

            var wordCountMap = wordCounter.GetWordCountMap(words);

            wordCountMap["apple"].Should().Be(2);
            wordCountMap["orange"].Should().Be(1);
            wordCountMap["oranges"].Should().Be(1);
        }

        [Fact]
        public void GetWordCountMap_EmptyList()
        {
            var words = new List<string>();

            var wordCountMap = wordCounter.GetWordCountMap(words);

            wordCountMap.Should().BeEmpty();
        }

        [Fact]
        public void GetWordCountMap_NullList()
        {
            Action action = () => wordCounter.GetWordCountMap(null);

            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
