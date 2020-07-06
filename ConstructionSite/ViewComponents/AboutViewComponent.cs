using ConstructionSite.DTO.ModelsDTO;
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
    public class AboutViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly   IHttpContextAccessor _httpContextAccessor;
        string _lang;
        public AboutViewComponent(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork=unitOfWork;
            _httpContextAccessor=httpContextAccessor;
            _lang=httpContextAccessor.getLang();
            
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
