using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.Areas.ConstructionAdmin.TagHelpers
{
    [HtmlTargetElement("p")]
    public class ServiceTagHelpers : TagHelper
    {
        [HtmlAttributeName("text")]
        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                if (Text.Length < 160)
                {
                    output.Content.SetHtmlContent(Text.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(Text.Substring(0, 160).ToString());
                }
            }
        }
    }
}