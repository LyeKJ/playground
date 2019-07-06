using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Extractors
{
    public interface IHtmlExtractorFactory
    {
        IHtmlExtractor CreateWebExtractor(Uri uri);

        IHtmlExtractor CreateRawExtractor(string html);
    }
}
