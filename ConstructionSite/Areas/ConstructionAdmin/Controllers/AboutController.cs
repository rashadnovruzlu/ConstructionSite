using ConstructionSite.DTO.AdminViewModels.About;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.About;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.About;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class AboutController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutFacade _aboutFacade;
        private readonly IAboutImageFacade _aboutImageFacade;

        #endregion Fields

        #region CTOR

        public AboutController(IUnitOfWork unitOfWork,
                               IWebHostEnvironment env,
                               IHttpContextAccessor httpContextAccessor,
                               IAboutFacade aboutFacade,
                               IAboutImageFacade aboutImageFacade)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
            _aboutFacade = aboutFacade;
            _aboutImageFacade = aboutImageFacade;
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

            var result = _aboutFacade.
                GetAll(_lang).
                   ToList(); ;
            if (result.Count < 1)
            {
                ModelState.AddModelError("", "NULL");
            }
            return View(result);
        }

        #endregion INDEX

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AboutAddViewModel aboutAddViewModel)
        {
            if (aboutAddViewModel == null)
            {
                ModelState.AddModelError("", "Data is null");
            }
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var resultAbout = await _aboutFacade.AddAsync(aboutAddViewModel);
            var resultImage = await aboutAddViewModel.FileData.SaveImageCollectionAsync(_env, "About", _unitOfWork);
            return await SaveAll(resultAbout, resultImage);
        }

        #endregion CREATE

        #region UPDATE

        public IActionResult Update(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "This data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            var result = _aboutFacade.GetForUpdate(id);
            if (result != null)
            {
                return View(result);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(AboutUpdateViewModel aboutUpdateViewModel)
        {
            if (aboutUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (aboutUpdateViewModel.files != null && aboutUpdateViewModel.ImageID != null)
            {
                try
                {
                    for (int i = 0; i < aboutUpdateViewModel.ImageID.Count; i++)
                    {
                        var image = _unitOfWork.imageRepository.Find(x => x.Id == aboutUpdateViewModel.ImageID[i]);
                        await aboutUpdateViewModel.files[i].UpdateAsyc(_env, image, "about", _unitOfWork);
                    }
                }
                catch
                {
                }
            }
            else if (aboutUpdateViewModel.files != null)
            {
                try
                {
                    var emptyImage = _unitOfWork.newsRepository.Find(x => x.Id == aboutUpdateViewModel.Id);
                    //var newsimageID = await _unitOfWork.newsImageRepository.FindAsync(x => x.NewsId == emptyImage.Id);
                    var imagesid = await aboutUpdateViewModel.files.SaveImageCollectionAsync(_env, "blog", _unitOfWork);
                    foreach (var item in imagesid)
                    {
                        var resultData = new NewsImage
                        {
                            NewsId = emptyImage.Id,
                            ImageId = item
                        };
                        await _unitOfWork.newsImageRepository.AddAsync(resultData);
                    }
                    await _unitOfWork.CommitAsync();
                }
                catch
                {
                }
            }
            var resultAbout = await _aboutFacade.Update(aboutUpdateViewModel);
            if (resultAbout.IsDone)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #endregion UPDATE

        #region DELETE

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 1)
            {
                ModelState.AddModelError("", "NULL");
            }
            if (_aboutFacade.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #endregion DELETE

        #region ::private::

        private async Task<IActionResult> SaveAll(RESULT<Entity.Models.About> resultAbout, List<int> resultImage)
        {
            if (resultAbout.IsDone && resultImage.Count > 0)
            {
                foreach (var item in resultImage)
                {
                    var resultAboutImageAddViewModel = new AboutImageAddViewModel
                    {
                        ImageId = item,
                        AboutId = resultAbout.Data.Id
                    };
                    await _aboutImageFacade.AddAsync(resultAboutImageAddViewModel);
                }
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Rollback();
                }
            }
            return View();
        }

        #endregion ::private::
    }
}