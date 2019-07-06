using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Extractors
{
    public class HtmlAgilityPackRawExtractor : HtmlAgilityPackBaseExtractor
    {
        internal HtmlAgilityPackRawExtractor(string html) : base(CreateHtmlDocument(html))
        {
        }

        private static HtmlDocument CreateHtmlDocument(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            return htmlDoc;
        }
    }
}
