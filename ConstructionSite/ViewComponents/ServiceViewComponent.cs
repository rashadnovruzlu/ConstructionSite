using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class ServiceViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _lang;
        public ServiceViewComponent(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork=unitOfWork;
            _httpContextAccessor=httpContextAccessor;
            _lang=httpContextAccessor.getLang();
        }
        public  IViewComponentResult Invoke()
        {
            var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubServices).Select(x=>new ServiceViewModel
                {
                    Id=x.Id,
                    Image=x.Image.Path,
                    Name=x.FindName(_lang),
                    Tittle=x.FindName(_lang)
                    
                }).ToList();
          
         return View(result);
                
                
        }
    }
}
