using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("p",Attributes ="service-index")]
    public class ServiceIndexTagHelper : TagHelper
    {
        
        public string ServiceIndex { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(ServiceIndex))
            {
                if (ServiceIndex.Length < 100)
                {
                    output.Content.SetHtmlContent(ServiceIndex.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(ServiceIndex.Substring(0, 100).ToString());
                }
            }
        }
    }
}