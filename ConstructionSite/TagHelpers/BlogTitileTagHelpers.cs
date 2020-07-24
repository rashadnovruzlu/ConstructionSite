using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("a")]
    public class BlogTitileTagHelpers:TagHelper
    {
        [HtmlAttributeName("blogtitle")]
        public string size { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(size))
            {
                if (size.Length < 27)
                {
                    output.Content.SetHtmlContent(size.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(size.Substring(0, 26).ToString());
                }
            }
        }
    }
}
