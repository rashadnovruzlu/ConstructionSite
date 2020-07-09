using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstructionSite.Controllers
{
    public class AboutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public AboutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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