using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    // [HtmlTargetElement("img")]
    public class ImageTagHelper : TagHelper
    {
        [HtmlAttributeName("imgwidth")]
        public int Size { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("width", Size);
        }
    }
}