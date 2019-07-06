using LyeKJ.SEOAnalyser.Constants;
using LyeKJ.SEOAnalyser.Core;
using LyeKJ.SEOAnalyser.Core.Extractors;
using LyeKJ.SEOAnalyser.Core.Tokenizers;
using LyeKJ.SEOAnalyser.Core.Utilities;
using LyeKJ.SEOAnalyser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LyeKJ.SEOAnalyser.Controllers
{
    public class HomeController : Controller
    {
        private const string AnalysisResultKey = "AnalysisResult";

        private readonly ITokenizer tokenizer;
        private readonly IHtmlExtractorFactory htmlExtractorFactory;
        private readonly IWordCounter wordCounter;

        public HomeController(ITokenizer tokenizer, IHtmlExtractorFactory htmlExtractorFactory, IWordCounter wordCounter)
        {
            this.tokenizer = tokenizer;
            this.htmlExtractorFactory = htmlExtractorFactory;
            this.wordCounter = wordCounter;
        }

        public ActionResult Index()
        {
            if (TempData.TryGetValue(AnalysisResultKey, out var analysisResult))
            {
                return View(analysisResult as AnalysisResult);
            }
            else
            {
                return View(new AnalysisResult());
            }
        }

        [HttpPost]
        public ActionResult SubmitAnalysis(AnalysisForm analysisForm)
        {
            if (analysisForm.IsHtml && string.IsNullOrWhiteSpace(analysisForm.HtmlTextToBeAnalysed))
            {
                ModelState.AddModelError(ValidationErrorConstants.HtmlTextIsRequired, "Html text is required.");
                return GetIndexView();
            }

            if (!analysisForm.IsHtml && !UrlUtility.IsValidUrl(analysisForm.UriToBeAnalysed))
            {
                ModelState.AddModelError(ValidationErrorConstants.UrlIsInvalid, "Url is empty or invalid (protocol prefix is required).");
                return GetIndexView();
            }

            IHtmlExtractor htmlExtractor;

            if (analysisForm.IsHtml)
            {
                htmlExtractor = htmlExtractorFactory.CreateRawExtractor(analysisForm.HtmlTextToBeAnalysed);
            }
            else
            {
                try
                {
                    htmlExtractor = htmlExtractorFactory.CreateWebExtractor(new Uri(analysisForm.UriToBeAnalysed));
                }
                catch (WebException)
                {
                    ModelState.AddModelError(ValidationErrorConstants.InaccessibleWebsite, "Website could not be reached.");
                    return GetIndexView();
                }
            }

            var texts = htmlExtractor.ExtractText();

            var tokens = tokenizer.Tokenize(texts, analysisForm.IsExcludingStopWords);

            var wordCountMap = wordCounter.GetWordCountMap(tokens).OrderByDescending(x => x.Value)
                                .ToDictionary(x => x.Key, x => x.Value);

            var metaKeywords = htmlExtractor.ExtractMetaKeywords();

            var metaKeywordMap = wordCountMap.Where(x => metaKeywords.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);

            var links = htmlExtractor.ExtractLinks();

            TempData[AnalysisResultKey] = new AnalysisResult
            {
                WordCountMap = wordCountMap,
                KeywordCountMap = metaKeywordMap,
                Links = links
            };

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private ActionResult GetIndexView()
        {
            return View(viewName: "Index", model: new AnalysisResult());
        }
    }
}