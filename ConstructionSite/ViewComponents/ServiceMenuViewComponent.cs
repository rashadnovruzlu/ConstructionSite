using ConstructionSite.DTO.FrontViewModels;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class ServiceMenuViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private  string _lang;
        public ServiceMenuViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            _lang = culture.Name;
        }
        public IViewComponentResult Invoke()
        {
          var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.SubServices)
                .Select(x=>new ServiceMenuViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang),
                   SubServices=x.SubServices.Select(z=>new SingleSubServiceViewModel
                   {
                       id=x.Id,
                       Name=x.FindName(_lang)
                   })
                   .ToList()
                    
                }).ToList();
            return View(result);
        }
    }
}
