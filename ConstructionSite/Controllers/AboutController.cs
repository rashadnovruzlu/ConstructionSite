using ConstructionSite.DTO.FrontViewModels.About;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    public class AboutController : Controller
    {
        private string                           _lang;
        private readonly IHttpContextAccessor    _httpContextAccessor;
        private SharedLocalizationService        _localizationHandle;
        private readonly IUnitOfWork             _unitOfWork;

        public AboutController(IUnitOfWork unitOfWork,
            SharedLocalizationService localizationHandle,
             IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _lang=_httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
            //_localizationHandle.GetLocalizationByKey(RESOURCEKEYS.)
        }

        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var data = _unitOfWork.AboutImageRepository
                    .GetAll()
                    .Include(x => x.Image)
                    .Include(x => x.About)
                    .Select(x => new AboutIndexViewModel
                    {
                        Id = x.AboutId,
                        Context = x.About.FindContent(_lang),
                        Title = x.About.FindTitle(_lang),
                        imagePath = x.Image.Path
                    }).ToList().OrderByDescending(x => x.Id).
                    FirstOrDefault();

            return View(data);
        }
        public IActionResult About(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var data = _unitOfWork.AboutImageRepository.GetAll()
                         .Include(x => x.Image)
                         .Include(x => x.About)
                         .Where(x=>x.AboutId==id)
                         .Select(x => new AboutIndexViewModel
                         {
                             Id = x.Id,
                             AboutID=x.AboutId,
                             Context = x.About.FindContent(_lang),
                             Title = x.About.FindTitle(_lang),
                             imagePath = x.Image.Path,
                             path=x.About.AboutImages.Select(y=>y.Image.Path).ToList()
                         }).ToList().OrderByDescending(x => x.Id)
                         .FirstOrDefault();
                        

            return View(data);
        }
    }
}