using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("blockquote")]
    public class TestimonialsTagHelper : TagHelper
    {
        [HtmlAttributeName("testimonialtext")]
        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                if (Text.Length < 300)
                {
                    output.Content.SetHtmlContent(Text.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(Text.Substring(0, 300).ToString());
                }
            }
        }
    }
}
