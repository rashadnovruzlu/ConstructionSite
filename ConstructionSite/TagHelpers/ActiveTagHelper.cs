using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("li",Attributes ="bsActive")]
    public class ActiveTagHelper:TagHelper
    {
        public string bsActive { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           output.Attributes.SetAttribute("class",$"{bsActive}");
        }
    }
}
