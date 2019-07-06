using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.Tokenizers
{
    public class Tokenizer : ITokenizer
    {
        private static readonly List<char> Punctuations =
            new List<char> { ' ', ',', '?', '.', '!', ';' };

        private readonly List<string> stopWords;

        public Tokenizer(string stopWordFilePath)
        {
            if (stopWordFilePath == null)
            {
                throw new ArgumentNullException(nameof(stopWordFilePath));
            }

            stopWords = File.ReadAllLines(stopWordFilePath).ToList();
        }

        public IList<string> Tokenize(IList<string> text, bool excludingStopWords = true)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var tokens = text.SelectMany(x => x.Split(Punctuations.ToArray(), StringSplitOptions.RemoveEmptyEntries));

            if (excludingStopWords)
            {
                tokens = tokens.Where(x => !stopWords.Contains(x.ToLowerInvariant()));
            }

            return tokens.ToList();
        }
    }
}
