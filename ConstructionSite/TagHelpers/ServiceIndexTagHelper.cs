using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("p")]
    public class ServiceIndexTagHelper : TagHelper
    {
        [HtmlAttributeName("indextext")]
        public string indextext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(indextext))
            {
                if (indextext.Length < 100)
                {
                    output.Content.SetHtmlContent(indextext.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(indextext.Substring(0, 100).ToString());
                }
            }
        }
    }
}