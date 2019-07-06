using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Extractors
{
    public class HtmlExtractorFactory : IHtmlExtractorFactory
    {
        public IHtmlExtractor CreateRawExtractor(string html)
        {
            return new HtmlAgilityPackRawExtractor(html);
        }

        public IHtmlExtractor CreateWebExtractor(Uri uri)
        {
            return new HtmlAgilityPackWebExtractor(uri);
        }
    }
}
