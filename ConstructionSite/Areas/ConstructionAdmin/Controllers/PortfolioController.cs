using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Portfolio;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Portfolio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{

    public class PortfolioController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPortfolioImageFacade _portfolioImageFacade;

        #endregion Fields

        #region CTOR

        public PortfolioController(IUnitOfWork unitOfWork,
                                   IHttpContextAccessor httpContextAccessor,
                                   IPortfolioImageFacade portfolioImageFacade)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _portfolioImageFacade = portfolioImageFacade;
            _lang = _httpContextAccessor.GetLanguages();
        }

        #endregion CTOR

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var result = _unitOfWork.portfolioRepository.GetAll()
            .Select(x => new PortfolioViewModel
            {
                Id = x.Id,
                Name = x.FindName(_lang)
            }).ToList();

            if (result.Count < 0)
            {
                ModelState.AddModelError("", "This is empty");
            }
            return View(result);
        }

        #endregion INDEX

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Portfolio portfolio, IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (portfolio == null)
            {
                ModelState.AddModelError("", "Portfolio is NULL");
            }

            var portfolioResult = await _unitOfWork.portfolioRepository.AddAsync(portfolio);
            PortfolioImageAddViewModel portfolioImageAddViewModel = new PortfolioImageAddViewModel();
            portfolioImageAddViewModel.PortfolioId = portfolio.Id;

            if (portfolioResult.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            else
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Data is Not Saved.");
            }
            return RedirectToAction("Index");
        }

        #endregion CREATE

        #region UPDATE

        public IActionResult Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var portfoliUpdateViewModel = _unitOfWork.portfolioRepository.GetById(id);
            if (portfoliUpdateViewModel == null)
            {
                ModelState.AddModelError("", "Data Is Null");
            }
            var portfoliUpdateViewModelResult = new PortfoliUpdateViewModel
            {
                id = portfoliUpdateViewModel.Id,
                NameAz = portfoliUpdateViewModel.NameAz,
                NameEn = portfoliUpdateViewModel.NameEn,
                NameRu = portfoliUpdateViewModel.NameRu
            };
            return View(portfoliUpdateViewModelResult);
        }

        [HttpPost]
        public IActionResult Update(PortfoliUpdateViewModel portfoliUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (portfoliUpdateViewModel == null)
            {
            }
            var portfoliUpdateViewModelresult = new Portfolio
            {
                Id = portfoliUpdateViewModel.id,
                NameAz = portfoliUpdateViewModel.NameAz,
                NameEn = portfoliUpdateViewModel.NameEn,
                NameRu = portfoliUpdateViewModel.NameRu
            };
            var result = _unitOfWork.portfolioRepository.Update(portfoliUpdateViewModelresult);
            if (!result.IsDone)
            {
                _unitOfWork.Rollback();

                ModelState.AddModelError("", "This is not successfull update");
            }
            else
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            return View(portfoliUpdateViewModel);
        }

        #endregion UPDATE

        #region DELETE

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            Portfolio portfolioResult = await _unitOfWork.portfolioRepository.GetByIdAsync(id);
            if (portfolioResult == null)
            {
                return RedirectToAction("Index");
            }

            var portfolioDeleteResult = await _unitOfWork.portfolioRepository.DeleteAsync(portfolioResult);
            if (!portfolioDeleteResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This portfolio was not delete");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion DELETE
    }
}