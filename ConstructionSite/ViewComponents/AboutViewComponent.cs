using ConstructionSite.DTO.FrontViewModels.About;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
          
            var aboutImageResult=_unitOfWork.AboutImageRepository.GetAll()
                .Select(x=>new AboutViewModel
                {
                    Id=x.Id,
                    AboutID=x.AboutId,
                    Content=x.About.FindContent(_lang),
                    Tittle=x.About.FindTitle(_lang),
                    Image=x.Image.Path,
                    imageId=x.ImageId
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
