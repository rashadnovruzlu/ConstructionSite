using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("blockquote",Attributes = "testimonial-text")]
    public class TestimonialsTagHelper : TagHelper
    {
       
        public string TestimonialText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(TestimonialText))
            {
                if (TestimonialText.Length < 300)
                {
                    output.Content.SetHtmlContent(TestimonialText.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(TestimonialText.Substring(0, 300).ToString());
                }
            }
        }
    }
}