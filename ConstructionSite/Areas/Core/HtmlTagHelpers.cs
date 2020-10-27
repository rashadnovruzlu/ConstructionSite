using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.Core
{
    public static class HtmlTagHelpers
    {
        public static void readerJavScrip(this IHtmlHelper htmlHelper, dynamic value)
        {
            dynamic aa = value;
        }

    }
}
