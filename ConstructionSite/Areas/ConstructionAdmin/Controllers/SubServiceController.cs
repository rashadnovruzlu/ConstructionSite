using ConstructionSite.DTO.AdminViewModels.SubService;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Services;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class SubServiceController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISubServiceFacade _subServiceFacade;

        #endregion Fields

        #region CTOR

        public SubServiceController(IUnitOfWork unitOfWork,
                                    IWebHostEnvironment env,
                                    IHttpContextAccessor httpContextAccessor,
                                    ISubServiceFacade subServiceFacade)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
            _subServiceFacade = subServiceFacade;
        }

        #endregion CTOR

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid");
            }

            var SubServiceImage = _unitOfWork.SubServiceImageRepository.GetAll()
                .Include(x => x.Image)
                .Include(x => x.SubService)
                .Select(x => new SubServiceViewModel
                {
                    Id = x.SubServiceId,
                    ImagePath = x.Image.Path,
                    Name = x.SubService.FindName(_lang),
                    Content = x.SubService.FindContent(_lang)
                })
                .ToList();
            if (SubServiceImage == null && SubServiceImage.Count < 1)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This is empty");
            }

            _unitOfWork.Dispose();
            return View(SubServiceImage);
        }

        #endregion INDEX

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid");
            }
            ViewBag.services = _subServiceFacade.GetServices(_lang);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SubServiceAddModel subServiceAddModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (subServiceAddModel == null)
            {
                ModelState.AddModelError("", "News is empty");
            }

            try
            {
                var resulBlogAddViewModel = await _subServiceFacade.Add(subServiceAddModel);
                var resultImage = await subServiceAddModel.file.SaveImageCollectionAsync(_env, "news", _unitOfWork);
                if (resulBlogAddViewModel.IsDone && resultImage.Count > 0)
                {
                    foreach (var item in resultImage)
                    {
                        var result = new SubServiceImage
                        {
                            ImageId = item,
                            SubServiceId = resulBlogAddViewModel.Data.Id
                        };
                        await _unitOfWork.SubServiceImageRepository.AddAsync(result);
                    }
                    if (await _unitOfWork.CommitAsync())
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {


            }
            return View();


        }

        #endregion CREATE

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            var resultUpdate = _subServiceFacade.GetForUpdate(id);
            return View(resultUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SubServiceUpdateViewModel subServiceUpdateViewModel)
        {
            try
            {
                if (subServiceUpdateViewModel == null)
                {
                    ModelState.AddModelError("", "This data not exists");
                }
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Models are not valid.");
                }
                if (subServiceUpdateViewModel.files != null && subServiceUpdateViewModel.ImageID != null)
                {
                    try
                    {
                        for (int i = 0; i < subServiceUpdateViewModel.ImageID.Count; i++)
                        {
                            var image = _unitOfWork.imageRepository.Find(x => x.Id == subServiceUpdateViewModel.ImageID[i]);
                            await subServiceUpdateViewModel.files[i].UpdateAsyc(_env, image, "about", _unitOfWork);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (subServiceUpdateViewModel.files != null)
                {
                    try
                    {
                        var emptyImage = _unitOfWork.SubServiceRepository.Find(x => x.Id == subServiceUpdateViewModel.Id);

                        var imagesid = await subServiceUpdateViewModel.files.SaveImageCollectionAsync(_env, "blog", _unitOfWork);
                        foreach (var item in imagesid)
                        {
                            var resultData = new SubServiceImage
                            {
                                SubServiceId = emptyImage.Id,
                                ImageId = item
                            };
                            await _unitOfWork.SubServiceImageRepository.AddAsync(resultData);
                        }
                        await _unitOfWork.CommitAsync();
                    }
                    catch
                    {
                    }
                }
                var resultAbout = await _subServiceFacade.Update(subServiceUpdateViewModel);
                if (resultAbout.IsDone)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {


            }
            return View(subServiceUpdateViewModel);
        }

        #endregion UPDATE

        #region DELETE

        public IActionResult Delete(int id)
        {
            if (_subServiceFacade.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        #endregion DELETE
    }
}