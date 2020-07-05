using ConstructionSite.Repository.Abstract;
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
        public ServiceViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public  IViewComponentResult Invoke()
        {
            var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubServices).ToList();
          
         return View(result);
                
                
        }
    }
}
