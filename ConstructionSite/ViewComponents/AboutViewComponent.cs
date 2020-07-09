using ConstructionSite.DTO.FrontViewModels.About;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace ConstructionSite.ViewComponents
{
    public class AboutViewComponent:ViewComponent
    {
        string                                  _lang;
        private readonly   IUnitOfWork          _unitOfWork;
        private readonly   IHttpContextAccessor _httpContextAccessor;
        
        public AboutViewComponent(IUnitOfWork unitOfWork, 
                                  IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork=unitOfWork;
            _httpContextAccessor=httpContextAccessor;
            _lang=httpContextAccessor.getLang();
            
        }
        public IViewComponentResult Invoke()
        {
            if (!ModelState.IsValid)
            {
               _httpContextAccessor.HttpContext.Response.StatusCode  = (int)HttpStatusCode.BadRequest;

               ModelState.AddModelError("", "BadRequest");
            }
            var aboutImageResult =    _unitOfWork.AboutImageRepository.GetAll()
               .Include(x=>x.About)
               .Include(x=>x.Image)
               .Select(y=> new AboutViewModel
               {
                  Tittle=y.About.FindTitle(_lang),
                  Content=y.About.FindContent(_lang),
                  Image=y.Image.Path,
                  Id=y.About.Id
               }).ToList()
               .FirstOrDefault();
            if (aboutImageResult==null)
            {
                ModelState.AddModelError("","data not exists ");
            }
                   
            return View(aboutImageResult);
        }
        }
}
