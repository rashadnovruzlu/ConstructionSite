﻿using ConstructionSite.DTO.FrontViewModels.Descriptions;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("meta",Attributes ="descriptions")]
    public class DescriptionTagHelper:TagHelper
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DescriptionTagHelper(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLang();

        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

         var result=_unitOfWork.AboutRepository.GetAll()
                .OrderByDescending(x=>x.Id)
                .Select(x=>new Description
                {
                    Content=x.FindTitle(_lang)
                })
                .FirstOrDefault();
           string str=result.Content;

            output.Attributes.Add("content",str);

        }
    }
}
