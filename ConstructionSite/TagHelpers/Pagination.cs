using ConstructionSite.Helpers.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    public class Pagination:TagHelper
    {
        public IUrlHelperFactory _urlHelperFactory;
        public Pagination(IUrlHelperFactory urlHelperFactory)
        {
            this._urlHelperFactory=urlHelperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo  PagingInfo { get; set; }
        public string PagingAction { get; set; }
    }
}
