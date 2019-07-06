using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core
{
    public class WordCounter : IWordCounter
    {
        public IDictionary<string, int> GetWordCountMap(IList<string> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            var wordCountMap = new Dictionary<string, int>();

            foreach (var word in words)
            {
                var normalizedWord = word.ToLowerInvariant();
                wordCountMap[normalizedWord] = wordCountMap.TryGetValue(normalizedWord, out var count) ? ++count : 1;
            }

            return wordCountMap;
        }
    }
}
