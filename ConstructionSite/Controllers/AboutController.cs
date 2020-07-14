using ConstructionSite.DTO.FrontViewModels.About;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Controllers
{
    public class AboutController : Controller
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        SharedLocalizationService _localizationHandle;
        private readonly IUnitOfWork _unitOfWork;

        public AboutController(IUnitOfWork unitOfWork,
            SharedLocalizationService localizationHandle,
             IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor=httpContextAccessor;
            _unitOfWork = unitOfWork;
            _localizationHandle = localizationHandle;
            //_localizationHandle.GetLocalizationByKey(RESOURCEKEYS.)
        }
        public IActionResult Index()
        {
        var data=_unitOfWork.AboutImageRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.About)
                .Select(x=>new AboutIndexViewModel
                {
                    Id=x.Id,
                    Context=x.About.FindContent(_lang),
                    Title=x.About.FindTitle(_lang),
                    imagePath=x.Image.Path
                });
            return View(data);
        }
    }
}