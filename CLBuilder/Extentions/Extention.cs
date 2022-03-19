using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Reflection;
using System.IO.Packaging;
using System.Windows.Markup;

namespace CLBuilder.Extentions
{
    static class Extention
    {
        /// <summary>
        /// Capitalizes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.String.</returns>
        public static string Capitalize(this string source)
        {
            if (source.IsNullOrEmpty())
                return source;

            return source.Substring(0, 1).ToUpper() + source.Substring(1);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static void LoadViewFromUri(this UserControl userControl, string baseUri)
        {
            try
            {
                var resourceLocator = new Uri(baseUri, UriKind.Relative);
                var exprCa = (PackagePart)typeof(Application).GetMethod("GetResourceOrContentPart", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { resourceLocator });
                var stream = exprCa.GetStream();
                var uri = new Uri((Uri)typeof(BaseUriHelper).GetProperty("PackAppBaseUri" , BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null), resourceLocator);
                var parserContext = new ParserContext
                {
                    BaseUri = uri
                };
                typeof(XamlReader).GetMethod("LoadBaml", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { stream, parserContext, userControl, true });

                stream.Dispose();
            }
            catch (Exception)
            {
                // Log
            }
        }
    }
}
