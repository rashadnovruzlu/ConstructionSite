using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Portfolio;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class PortfolioController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPortfolioImageFacade _portfolioImageFacade;
        private readonly IPortfolioFacade _portfolioFacade;

        #endregion Fields

        #region CTOR

        public PortfolioController(IUnitOfWork unitOfWork,
                                   IHttpContextAccessor httpContextAccessor,
                                   IPortfolioImageFacade portfolioImageFacade,
                                   IPortfolioFacade portfolioFacade,
                                   IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _portfolioFacade = portfolioFacade;
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
            var resultViewModel = _portfolioFacade.GetAll(_lang);

            if (resultViewModel == null)
            {
                ModelState.AddModelError("", "This is empty");
            }
            return View(resultViewModel);
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
        public async Task<IActionResult> Add(PortfolioAddModel portfolioAddModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (portfolioAddModel == null)
            {
                ModelState.AddModelError("", "Portfolio is NULL");
            }

            try
            {
                var portfolioResult = await _portfolioFacade.Add(portfolioAddModel);
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Rollback();
                }
            }
            catch
            {
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
            var portfoliUpdateViewModelResult = _portfolioFacade.GetForUpdate(id);
            if (portfoliUpdateViewModelResult == null)
            {
                ModelState.AddModelError("", "Data Is Null");
            }

            return View(portfoliUpdateViewModelResult);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PortfoliUpdateViewModel portfoliUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (portfoliUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
            }

            try
            {
                if (portfoliUpdateViewModel.files != null && portfoliUpdateViewModel.ImageID != null)
                {
                    try
                    {
                        for (int i = 0; i < portfoliUpdateViewModel.ImageID.Count; i++)
                        {
                            var image = _unitOfWork.imageRepository.Find(x => x.Id == portfoliUpdateViewModel.ImageID[i]);
                            await portfoliUpdateViewModel.files[i].UpdateAsyc(_env, image, "blog", _unitOfWork);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (portfoliUpdateViewModel.files != null)
                {
                    var emptyImage = _unitOfWork.portfolioRepository.Find(x => x.Id == portfoliUpdateViewModel.id);

                    var imagesid = await portfoliUpdateViewModel.files.SaveImageCollectionAsync(_env, "portfolio", _unitOfWork);
                    foreach (var item in imagesid)
                    {
                        var resultData = new PortfolioImage
                        {
                            PortfolioId = emptyImage.Id,
                            ImageId = item
                        };
                        await _unitOfWork.PortfolioImageRepostory.AddAsync(resultData);
                    }
                    await _unitOfWork.CommitAsync();
                }
                var resultPortfolio = await _portfolioFacade.Update(portfoliUpdateViewModel);
                if (resultPortfolio.IsDone)
                {
                    if (await _unitOfWork.CommitAsync())
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
            }
            return RedirectToAction("Index");
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
            if (await _portfolioFacade.Delete(id))
            {
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        #endregion DELETE
    }
}