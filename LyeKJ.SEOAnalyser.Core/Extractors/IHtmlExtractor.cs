using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Extractors
{
    public interface IHtmlExtractor
    {
        IList<string> ExtractText();

        IList<string> ExtractLinks();

        IList<string> ExtractMetaKeywords();
    }
}
