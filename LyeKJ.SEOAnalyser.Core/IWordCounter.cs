using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core
{
    public interface IWordCounter
    {
        IDictionary<string, int> GetWordCountMap(IList<string> words);
    }
}
