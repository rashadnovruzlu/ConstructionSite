using ConstructionSite.Helpers.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
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
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper=_urlHelperFactory.GetUrlHelper(ViewContext);
            var result=new TagBuilder("ul");
            result.Attributes["class"]= "page-numbers";
            for (int i = 1; i < PagingInfo.TotalPage(); i++)
            {
                var li=new TagBuilder("li");
                if (i==1)
                {
                    var span=new TagBuilder("span");
                    span.Attributes["class"]= "page-numbers current";
                    li.InnerHtml.Append(span.ToString());
                   
                }
                else
                {
                    var a=new TagBuilder("a");
                    a.Attributes["class"]= "page-numbers";
                    a.Attributes["href"]=urlHelper.Action(PagingAction,new { p=i});
                    a.InnerHtml.Append(i.ToString());
                    li.InnerHtml.Append(a.ToString());

                }
                result.InnerHtml.Append(li.ToString());
            }
         
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
