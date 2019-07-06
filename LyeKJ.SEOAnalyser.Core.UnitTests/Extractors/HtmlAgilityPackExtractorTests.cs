using FluentAssertions;
using LyeKJ.SEOAnalyser.Core.Extractors;
using LyeKJ.SEOAnalyser.Core.UnitTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LyeKJ.SEOAnalyser.Core.UnitTests.Extractors
{
    public class HtmlAgilityPackExtractorTests
    {
        private static readonly Uri TestUrl = new Uri("http://example.com");
        private static readonly string HtmlFilePath = Path.Combine(LocalFileHelper.GetAssemblyDirectory<HtmlAgilityPackExtractorTests>(),
                "Resources/Example.html");

        private readonly IHtmlExtractor webExtractor;
        private readonly IHtmlExtractor rawExtractor;

        public HtmlAgilityPackExtractorTests()
        {
            var factory = new HtmlExtractorFactory();
            webExtractor = factory.CreateWebExtractor(TestUrl);

            var html = File.ReadAllText(HtmlFilePath);
            rawExtractor = factory.CreateRawExtractor(html);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ExtractText(bool isHtml)
        {
            var actualTexts = (isHtml ? webExtractor : rawExtractor).ExtractText();

            var expectedTexts = new List<string>
            {
                "example domain",
                "example domain",
                "this domain is established to be used for illustrative examples in documents you may use this    domain in examples without prior coordination or asking for permission",
                "more information"
            };

            actualTexts.Should().BeEquivalentTo(expectedTexts);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ExtractLinks(bool isHtml)
        {
            var actualLinks = (isHtml ? webExtractor : rawExtractor).ExtractLinks();

            var expectedLinks = new List<string>
            {
                "http://www.iana.org/domains/example"
            };

            actualLinks.Should().BeEquivalentTo(expectedLinks);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ExtractMetaKeywords(bool isHtml)
        {
            var actualKeywords = (isHtml ? webExtractor : rawExtractor).ExtractMetaKeywords();

            var expectedKeywords = new List<string>();

            actualKeywords.Should().BeEquivalentTo(expectedKeywords);
        }
    }
}
