using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LyeKJ.SEOAnalyser.Core.UnitTests.TestHelpers
{
    public static class LocalFileHelper
    {
        public static string GetAssemblyDirectory<T>()
        {
            var codeBase = Assembly.GetAssembly(typeof(T)).CodeBase;

            return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(codeBase).Path));
        }
    }
}
