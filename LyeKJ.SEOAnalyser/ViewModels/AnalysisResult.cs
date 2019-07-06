using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyeKJ.SEOAnalyser.ViewModels
{
    public class AnalysisResult
    {
        public AnalysisResult()
        {
            WordCountMap = new Dictionary<string, int>();
            KeywordCountMap = new Dictionary<string, int>();
            Links = new List<string>();
        }

        public Dictionary<string, int> WordCountMap { get; set; }

        public Dictionary<string, int> KeywordCountMap { get; set; }

        public IList<string> Links { get; set; }
    }
}