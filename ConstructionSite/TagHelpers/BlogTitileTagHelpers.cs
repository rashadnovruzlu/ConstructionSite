using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("h1", Attributes = "blog-title")]
    public class BlogTdTitleTagHelpers : TagHelper
    {
        public string BlogTitle { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(BlogTitle))
            {
                if (BlogTitle.Length < 27)
                {
                    output.Content.SetHtmlContent(BlogTitle.ToString());
                }
                else
                {
                    output.Content.SetHtmlContent(BlogTitle.Substring(0, 26).ToString());
                }
            }
        }
    }
    //[HtmlTargetElement("td", Attributes = "blog-title")]
    //public class BlogTdTitleTagHelpers : TagHelper
    //{
    //    public string BlogTitle { get; set; }

    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        if (!string.IsNullOrEmpty(BlogTitle))
    //        {
    //            if (BlogTitle.Length < 27)
    //            {
    //                output.Content.SetHtmlContent(BlogTitle.ToString());
    //            }
    //            else
    //            {
    //                output.Content.SetHtmlContent(BlogTitle.Substring(0, 26).ToString());
    //            }
    //        }
    //    }
    //}
}