using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConstructionSite.Helpers.Interfaces;
using ConstructionSite.Helpers.Constants;

namespace ConstructionSite.Controllers
{
    public class AboutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISharedLocalizationHandle _localizationHandle;

        public AboutController(IUnitOfWork unitOfWork, ISharedLocalizationHandle localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _localizationHandle = localizationHandle;
            //_localizationHandle.GetLocalizationByKey(RESOURCEKEYS.)
        }
        //public async Task<IActionResult> Index()
        //{ 
        // var data= await  _unitOfWork.AboutRepository.GetAllAsync();
        //  var about=  data.AsQueryable().Include(x=>x.AboutImages)
        //        .ThenInclude(x=>x.Image);
        //        return View();
        //}
    }
}