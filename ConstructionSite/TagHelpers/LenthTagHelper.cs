using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("p")]
    public class LenthTagHelper : TagHelper
    {
        [HtmlAttributeName("size")]
        public string size { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(size))
            {
                if (size.Length < 110)
                {
                    output.Content.SetHtmlContent(size.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(size.Substring(0, 110).ToString());
                }
            }
        }
    }
}