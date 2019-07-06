using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Extractors
{
    public class HtmlAgilityPackBaseExtractor : IHtmlExtractor
    {
        private const string TextXPath = @"//*[not(self::script|self::style)]/text()";
        private const string LinkXPath = @"//a[@href]";
        private const string MetaTagXPath = @"//meta";
        private const string SpecialCharactersRegex = @"\r\n?|\n|\t|[^a-zA-Z0-9 ]";

        private readonly HtmlDocument htmlDoc;

        internal HtmlAgilityPackBaseExtractor(HtmlDocument htmlDoc)
        {
            this.htmlDoc = htmlDoc;
        }

        public virtual IList<string> ExtractLinks()
        {
            return htmlDoc.DocumentNode.SelectNodes(LinkXPath)?.Select(x => x.GetAttributeValue("href", string.Empty))
                .Where(x => Uri.IsWellFormedUriString(x, UriKind.Absolute) && !x.StartsWith("tel:"))
                .ToList() ?? Enumerable.Empty<string>().ToList();
        }

        public virtual IList<string> ExtractMetaKeywords()
        {
            return htmlDoc.DocumentNode.SelectNodes(MetaTagXPath)?
                .Where(x => x.GetAttributeValue("name", string.Empty) == "keywords")
                .Select(x => x.GetAttributeValue("content", string.Empty).ToLowerInvariant())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToList() ?? Enumerable.Empty<string>().ToList();
        }

        public virtual IList<string> ExtractText()
        {
            var texts = htmlDoc.DocumentNode.SelectNodes(TextXPath)?
                .Select(x => WebUtility.HtmlDecode(x.InnerText).ToLowerInvariant())
                .ToList() ?? Enumerable.Empty<string>().ToList();

            return RemoveSpecialCharacters(texts);
        }

        private static IList<string> RemoveSpecialCharacters(IList<string> texts)
        {
            return texts.AsParallel().Select(x => Regex.Replace(x, SpecialCharactersRegex, string.Empty, RegexOptions.Compiled).Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
        }
    }
}
