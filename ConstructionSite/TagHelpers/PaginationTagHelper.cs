using ConstructionSite.Helpers.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("div",Attributes ="page-model")]
    public class PaginationTagHelper:TagHelper
    {
        public IUrlHelperFactory _urlHelperFactory;
        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this._urlHelperFactory=urlHelperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo  PageModel { get; set; }
        public string PagingAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper=_urlHelperFactory.GetUrlHelper(ViewContext);
            var result=new TagBuilder("ul");
            result.AddCssClass("page-numbers");
        
            for (int i = 1; i <= PageModel.TotalPage(); i++)
            {
               var li = new TagBuilder("li");

               
                if (i == PageModel.CurrentPage)
                {
                    var myli=new TagBuilder("li");
                    var span = new TagBuilder("span");
                    span.AddCssClass("page-numbers current");
                    span.InnerHtml.Append(i.ToString());
                    myli.InnerHtml.AppendHtml(span);
                    result.InnerHtml.AppendHtml(myli);
                }
                else
                {
                    var a = new TagBuilder("a");
                    a.AddCssClass("page-numbers");
                    a.Attributes["href"] = urlHelper.Action(PagingAction, new { page = i });
                    a.InnerHtml.Append(i.ToString());
                    li.InnerHtml.AppendHtml(a);
                    result.InnerHtml.AppendHtml(li);
                }
               
    
            }
           
            output.Content.AppendHtml(result);
        }
    }
}
