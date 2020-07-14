using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Controllers
{
    public class ServicesController : Controller
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public ServicesController(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
        }
        public async  Task<IActionResult> Index(int id)
        {


            var data = await _unitOfWork.ServiceRepository.GetAllAsync();
            data.AsQueryable().Include(x => x.SubServices)
                .ThenInclude(x=>x.SubServiceImages)
                .ThenInclude()





            return View();
        }


    }
}