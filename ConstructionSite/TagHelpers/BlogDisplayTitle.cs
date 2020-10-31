using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("h1", Attributes = "show-content")]
    public class BlogDisplayTitle : TagHelper
    {
        public string ShowContent { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(ShowContent))
            {
                if (ShowContent.Length < 100)
                {
                    output.Content.SetHtmlContent(ShowContent.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(ShowContent.Substring(0, 99).ToString());
                }
            }
        }
    }
}
