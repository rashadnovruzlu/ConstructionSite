using ConstructionSite.DTO.FrontViewModels;
using ConstructionSite.Repository.Abstract;
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
        public ServiceMenuViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
          var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.SubServices)
                .Select(x=>new ServiceMenuViewModel
                {
                    Id=x.Id,
                    NameAz=x.NameAz,
                    NameEn=x.NameEn,
                    NameRu=x.NameRu,
                    SubServices=x.SubServices
                }).ToList();
            return View(result);
        }
    }
}
