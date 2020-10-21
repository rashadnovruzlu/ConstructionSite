using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    
    [HtmlTargetElement("p",Attributes ="blog-content")]
    public class BlogContentTagHelper : TagHelper
    {
       
        public string BlogContent { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(BlogContent))
            {
                if (BlogContent.Length < 1166)
                {
                    output.Content.SetHtmlContent(BlogContent.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(BlogContent.Substring(0, 1165).ToString());
                }
            }
        }
    }
}
