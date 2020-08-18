using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("p")]
    public class NewsTagHelpers : TagHelper
    {
        [HtmlAttributeName("blogtext")]
        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                if (Text.Length < 130)
                {
                    output.Content.SetHtmlContent(Text.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(Text.Substring(0, 150).ToString());
                }
            }
        }
    }
}