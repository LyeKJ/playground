using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LyeKJ.SEOAnalyser.ViewModels
{
    public class AnalysisForm
    {
        [DisplayName("Uri")]
        public string UriToBeAnalysed { get; set; }

        [DisplayName("Html Text")]
        [AllowHtml]
        public string HtmlTextToBeAnalysed { get; set; }

        public bool IsHtml { get; set; }

        public bool IsExcludingStopWords { get; set; }

        public AnalysisForm()
        {
            IsExcludingStopWords = true;
        }
    }
}