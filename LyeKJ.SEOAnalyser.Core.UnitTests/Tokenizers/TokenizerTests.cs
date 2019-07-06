using FluentAssertions;
using LyeKJ.SEOAnalyser.Core.Tokenizers;
using LyeKJ.SEOAnalyser.Core.UnitTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LyeKJ.SEOAnalyser.Core.UnitTests.Tokenizers
{
    public class TokenizerTests
    {
        private static readonly string StopWordFilePath = Path.Combine(LocalFileHelper.GetAssemblyDirectory<TokenizerTests>(),
                "Resources/stopwords.txt");

        private readonly Tokenizer tokenizer;

        public TokenizerTests()
        {
            tokenizer = new Tokenizer(StopWordFilePath);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Tokenize(List<string> sentences, List<string> expectedTokens, bool excludeStopwords)
        {
            var tokens = tokenizer.Tokenize(sentences, excludeStopwords);

            tokens.Should().BeEquivalentTo(expectedTokens);
        }

        [Fact]
        public void Tokenize_NullList()
        {
            Action action = () => tokenizer.Tokenize(null);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Tokenize_Constructor()
        {
            Action action = () => new Tokenizer(null);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<string>
                    {
                        "The quick brown fox jumps over the lazy dog",
                        "Tokenizer? tokenizes!",
                        "Sentences."
                    },
                    new List<string>
                    {
                        "quick", "brown", "fox", "jumps", "lazy", "dog",
                        "Tokenizer", "tokenizes", "Sentences"
                    },
                    true
                },
                new object[]
                {
                    new List<string>
                    {
                        "The quick brown fox jumps over the lazy dog",
                        "Tokenizer? tokenizes!",
                        "Sentences."
                    },
                    new List<string>
                    {
                        "The", "over", "the",
                        "quick", "brown", "fox", "jumps", "lazy", "dog",
                        "Tokenizer", "tokenizes", "Sentences"
                    },
                    false
                },
                new object[]
                {
                    new List<string>(),
                    new List<string>(),
                    false
                },
                new object[]
                {
                    new List<string>(),
                    new List<string>(),
                    true
                }
            };
    }
}
