using ConstructionSite.DTO.AdminViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace ConstructionSite.Areas.ConstructionAdmin.TagHelpers
{
    [HtmlTargetElement(Attributes = "langs")]
    public class ServiceTagHelpers:TagHelper
    {
        public ServiceViewModel langs { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
        }
    }
}
