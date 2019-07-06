using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Tokenizers
{
    public interface ITokenizer
    {
        IList<string> Tokenize(IList<string> text, bool excludingStopWords = true);
    }
}
