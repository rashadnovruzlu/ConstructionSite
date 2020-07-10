﻿using ConstructionSite.DTO.AdminViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace ConstructionSite.Areas.ConstructionAdmin.TagHelpers
{
    [HtmlTargetElement("p")]
    public class ServiceTagHelpers:TagHelper
    {
        [HtmlAttributeName("text")]
        public string Text { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                if (Text.Length <160)
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
