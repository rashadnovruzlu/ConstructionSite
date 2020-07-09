using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace ConstructionSite.ViewComponents
{
    public class ServiceMenuViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private  string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServiceMenuViewComponent(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork=unitOfWork;
             
            _httpContextAccessor=httpContextAccessor;
           _lang= _httpContextAccessor.getLang();
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
