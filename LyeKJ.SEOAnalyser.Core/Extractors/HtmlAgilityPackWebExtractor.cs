using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Extractors
{
    public class HtmlAgilityPackWebExtractor : HtmlAgilityPackBaseExtractor
    {
        private readonly Uri uri;

        internal HtmlAgilityPackWebExtractor(Uri uri) :
            base(new HtmlWeb().Load(uri))
        {
            this.uri = uri;
        }

        public override IList<string> ExtractLinks()
        {
            return base.ExtractLinks().Where(x => !x.Contains(uri.Host))
                .ToList();
        }
    }
}
