﻿using ConstructionSite.DTO.FrontViewModels.Service;
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
    [HtmlTargetElement("td", Attributes = "current-id")]
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

        public int CurrentId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var result = _unitOfWork.ServiceRepository.GetAll()
               .Select(x => x.SubServices.Select(x => x.FindName(_lang)));
            TagBuilder Li = new TagBuilder("li");
            foreach (var item in result)
            {
                foreach (var items in item)
                {
                    Li.InnerHtml.AppendHtml(items);
                }
            }
            output.PreContent.AppendHtml(Li);



        }
    }
}
