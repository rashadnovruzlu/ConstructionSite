using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Services;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("li", Attributes = "items")]
    public class SubServiceShowTagHelper : TagHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        string _lang;
        private IHttpContextAccessor _httpContextAccessor;
        public SubServiceShowTagHelper(IUnitOfWork unitOfWork,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }

        public List<string> Items { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder Li = new TagBuilder("li");
            // Li.InnerHtml.AppendHtml()


        }
    }
}
