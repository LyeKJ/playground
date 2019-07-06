using FluentAssertions;
using LyeKJ.SEOAnalyser.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LyeKJ.SEOAnalyser.Core.UnitTests.Utilities
{
    public class UrlUtilityTests
    {
        [Theory]
        [InlineData(@"http://example.com", true)]
        [InlineData(@"https://example.com", true)]
        [InlineData("abc", false)]
        [InlineData("", false)]
        public void IsValidUrl(string url, bool expectedValidationResult)
        {
            var actualValidationResult = UrlUtility.IsValidUrl(url);

            actualValidationResult.Should().Be(expectedValidationResult);
        }
    }
}
