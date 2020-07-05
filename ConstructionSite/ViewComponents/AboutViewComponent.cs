using ConstructionSite.DTO.ModelsDTO;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class AboutViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public AboutViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
           var result=    _unitOfWork.AboutImageRepository.GetAll()
               .Include(x=>x.About)
               .Include(x=>x.Image)
               .Select(y=> new AboutDTO
               {
                   TitleAz=y.About.ContentAz,
                   TitleEn=y.About.TittleEn,
                   TitleRu=y.About.TittleRu,
                   ContentAz=y.About.ContentAz,
                   ContentEn=y.About.ContentEn,
                   ContentRu=y.About.ContentRu,
                   image=y.Image.Path
               }).ToList()
               .FirstOrDefault();
                   
            return View(result);
        }
        }
}
