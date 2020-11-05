using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class GaleryController : CoreController
    {
        #region ::FILDS::

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IGaleryFacade _galeryFacade;
        private readonly IGaleryFileFacde _galeryFileFacde;

        #endregion ::FILDS::

        #region ::CTOR::

        public GaleryController(IGaleryFacade galeryFacade, IGaleryFileFacde galeryFileFacde,
            IWebHostEnvironment env, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _galeryFacade = galeryFacade;
            _galeryFileFacde = galeryFileFacde;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
            _env = env;
            _unitOfWork = unitOfWork;
        }

        #endregion ::CTOR::

        public IActionResult Index()
        {
            var result = _galeryFacade.GetAll(_lang);
            return View(result);
        }

        #region ::ADD::

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GaleryAddViewModel galeryAddViewModel)
        {


            if (galeryAddViewModel.viodPath != null | galeryAddViewModel.files != null)
            {
                try
                {
                    var resultGalery = await _galeryFacade.Add(galeryAddViewModel);
                    if (await _unitOfWork.CommitAsync())
                    {
                        if (galeryAddViewModel.viodPath != null)
                        {
                            GaleryVido galeryVido = new GaleryVido
                            {
                                GaleryId = resultGalery.Data.Id,
                                VidoPath = galeryAddViewModel.viodPath
                            };
                            await _unitOfWork.GaleryVidoResptory.AddAsync(galeryVido);
                        }
                        else if (galeryAddViewModel.files != null)
                        {
                            var resultImage = await galeryAddViewModel.files.SaveImageCollectionAsync(_env, galeryAddViewModel.viodPath, "galery", _unitOfWork);
                            if (resultGalery.IsDone && resultImage.Count > 0)
                            {

                                await GaleryFileSaveWithImageAndGalery(resultGalery, resultImage);
                                return RedirectToAction("Index");
                            }
                        }
                    }





                }
                catch
                {
                }
            }
            if (await _unitOfWork.CommitAsync())
            {
                return RedirectToAction("Index");
            }
            return View();
        }



        #endregion ::ADD::

        #region ::UPDATE::

        public IActionResult Update(int id)
        {
            var resultFindById = _galeryFacade.GetForUpdate(id);
            return View(resultFindById);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GaleryUpdateViewModel galeryUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (galeryUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
            }

            if (galeryUpdateViewModel.files != null && galeryUpdateViewModel.ImageID != null)
            {
                try
                {
                    for (int i = 0; i < galeryUpdateViewModel.ImageID.Count; i++)
                    {
                        var image = _unitOfWork.imageRepository.Find(x => x.Id == galeryUpdateViewModel.ImageID[i]);
                        await galeryUpdateViewModel.files[i].UpdateAsyc(_env, image, "galery", _unitOfWork);
                    }
                }
                catch
                {
                }
            }
            else if (galeryUpdateViewModel.files != null)
            {
                try
                {
                    var emptyImage = _unitOfWork.GaleryRepstory.Find(x => x.Id == galeryUpdateViewModel.Id);

                    var imagesid = await galeryUpdateViewModel.files.SaveImageCollectionAsync(_env, "galery", _unitOfWork);
                    foreach (var item in imagesid)
                    {
                        var resultData = new GaleryFile
                        {
                            GaleryId = emptyImage.Id,
                            ImageId = item
                        };
                        await _unitOfWork.GaleryFileRepstory.AddAsync(resultData);
                    }
                    await _unitOfWork.CommitAsync();
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(galeryUpdateViewModel.VidoPath))
            {
                await _galeryFacade.GetAndUpdate(galeryUpdateViewModel.Id, galeryUpdateViewModel.VidoPath);
            }
            var resultGalery = await _galeryFacade.Update(galeryUpdateViewModel);
            if (resultGalery.IsDone)
            {
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        #endregion ::UPDATE::

        #region ::DELETE::

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
            }
            var result = await _galeryFacade.Delete(id);
            if (result.IsDone)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #endregion ::DELETE::

        #region ::private::


        private async Task GaleryFileSaveWithImageAndGalery(RESULT<Galery> resultGalery, List<int> resultImage)
        {
            foreach (var item in resultImage)
            {
                GaleryFileAddViewModel galeryFileAddViewModel = new GaleryFileAddViewModel
                {
                    ImageId = item,
                    GaleryId = resultGalery.Data.Id,

                };
                await _galeryFileFacde.Add(galeryFileAddViewModel);
            }
            await _unitOfWork.CommitAsync();
        }

        #endregion ::private::
    }
}