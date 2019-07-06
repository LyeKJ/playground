using FluentAssertions;
using LyeKJ.SEOAnalyser.Core.Extractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LyeKJ.SEOAnalyser.Core.UnitTests.Extractors
{
    public class HtmlAgilityPackWebExtractorTests
    {
        private static readonly Uri TestUrl = new Uri("http://example.com");

        private readonly IHtmlExtractor extractor;

        public HtmlAgilityPackWebExtractorTests()
        {
            var factory = new HtmlExtractorFactory();
            extractor = factory.CreateWebExtractor(TestUrl);
        }

        [Fact]
        public void ExtractText()
        {
            var actualTexts = extractor.ExtractText();

            var expectedTexts = new List<string>
            {
                "example domain",
                "example domain",
                "this domain is established to be used for illustrative examples in documents you may use this    domain in examples without prior coordination or asking for permission",
                "more information"
            };

            actualTexts.Should().BeEquivalentTo(expectedTexts);
        }

        [Fact]
        public void ExtractLinks()
        {
            var actualLinks = extractor.ExtractLinks();

            var expectedLinks = new List<string>
            {
                "http://www.iana.org/domains/example"
            };

            actualLinks.Should().BeEquivalentTo(expectedLinks);
        }

        [Fact]
        public void ExtractMetaKeywords()
        {
            var actualKeywords = extractor.ExtractMetaKeywords();

            var expectedKeywords = new List<string>();

            actualKeywords.Should().BeEquivalentTo(expectedKeywords);
        }
    }
}
