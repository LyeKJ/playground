using Autofac;
using Autofac.Integration.Mvc;
using LyeKJ.SEOAnalyser.Core;
using LyeKJ.SEOAnalyser.Core.Extractors;
using LyeKJ.SEOAnalyser.Core.Tokenizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LyeKJ.SEOAnalyser
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterServices();
        }

        private void RegisterServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var stopWordFilePath = HttpContext.Current.Server.MapPath("~/Resources/stopwords.txt");

            builder.Register(c => new Tokenizer(stopWordFilePath)).As<ITokenizer>().SingleInstance();
            builder.Register(c => new HtmlExtractorFactory()).As<IHtmlExtractorFactory>().SingleInstance();
            builder.Register(c => new WordCounter()).As<IWordCounter>().SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
