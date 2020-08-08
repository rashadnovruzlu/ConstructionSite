using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.TagHelpers
{
    [HtmlTargetElement("meta", Attributes = "metapage")]
    public class MetaTagHelper:TagHelper
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
      
        public MetaTagHelper(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLanguages();
           
        }
       
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            StringBuilder stringBuilder=new StringBuilder();
            var result = _unitOfWork.ServiceRepository.GetAll()
              .Include(x => x.SubServices)
              .Select(x => new ServiceMetaData
              {
                  Name = x.FindName(_lang)
              })
              .ToList();
            foreach (var item in result)
            {
                stringBuilder.Append(item.Name+",");
            }
            stringBuilder.Remove(stringBuilder.Length-1,1);

            output.Attributes.Add("content",stringBuilder.ToString());
         
        }
    }
}
