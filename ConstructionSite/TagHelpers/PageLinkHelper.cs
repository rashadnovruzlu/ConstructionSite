using ConstructionSite.Helpers.Paginations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("div",Attributes ="page-model")]
    public class PageLinkHelper:TagHelper
    {
        private    IUrlHelperFactory _urlHelperFactory;
        public PageLinkHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory=urlHelperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageMode { get; set; }
        public string PageAction { get; set; }
    }
}
