using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
  
    public class PortfolioController : Controller
    {
        private string                        _lang;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PortfolioController(IUnitOfWork unitOfWork,
                                   IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor=httpContextAccessor;
            _lang= _httpContextAccessor.getLang();
        }
        [HttpGet]
        public  IActionResult Index()
        {

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
                var result=  _unitOfWork.portfolioRepository.GetAll()
                .Include(x=>x.Projects)
                .Select(x=>new PortfolioViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang),
                    ProjectViewModel = x.Projects.Select(y=>new ProjectViewModel
                    {
                        Id=y.Id,
                        Content=y.FindContent(_lang),
                        Name=y.FindName(_lang)
                        

                        
                    }).ToList()
                }).ToList();
            if (result.Count<0)
            {
                return Json(new
                {
                    message = "this is empty"
                });
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
            if (portfolio == null)
            {
                return Json(new
                {
                    message = "data is null"
                });
            }
            var portfolioResult= await _unitOfWork.portfolioRepository.AddAsync(portfolio);
            if (portfolioResult.IsDone)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("","Data Not Save");
            }
            return View();
        }
        public IActionResult Update(int id)
        {
            return View();
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Portfolio portfolio = await _unitOfWork.portfolioRepository.GetByIdAsync(id);
            if (portfolio == null)
            {
                return RedirectToAction("Index");

            }
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
            var result=  await _unitOfWork.portfolioRepository.DeleteAsync(portfolio);
            if (!result.IsDone)
            {
                ModelState.AddModelError("", "this portfolio was not delete");
            }
            else
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return View(id);
        }
    }
}
