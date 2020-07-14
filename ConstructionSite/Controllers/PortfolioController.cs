
using ConstructionSite.DTO.FrontViewModels.Portfoli;
using ConstructionSite.DTO.FrontViewModels.Portfolio;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace ConstructionSite.Controllers
{
    public class PortfolioController : Controller
    {

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;


        public PortfolioController(IUnitOfWork unitOfWork,
                              IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _lang = _httpContextAccessor.getLang();
        }
        public IActionResult Index()
        {
            
            var result=_unitOfWork.portfolioRepository.GetAll()
                .Select(x=>new PortfolioViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang),
                    
                    
                }).ToList();
            return View(result);
        }

        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Project(int id)
        {

        var result=  _unitOfWork.projectImageRepository.GetAll()
                .Where(x=>x.ProjectId==id)
                .Include(x=>x.Project)
                .Include(x=>x.Image)
                .Select(x=>new PoftfiloProjectViewModel
                {
                    id=x.Project.Id,
                    Name=x.Project.FindName(_lang),
                    Imagepath=x.Image.Path
                })
                .ToList();
            return View(result);
        }
    }
}
