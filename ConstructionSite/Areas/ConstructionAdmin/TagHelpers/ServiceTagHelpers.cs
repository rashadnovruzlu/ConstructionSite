using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.TagHelpers
{
    public class ServiceTagHelpers:TagHelper
    {
        public string BsButton { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           output.Attributes.SetAttribute("class",)
        }
    }
}
