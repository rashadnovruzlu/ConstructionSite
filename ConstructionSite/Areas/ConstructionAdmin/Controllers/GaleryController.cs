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
            return View();
        }

        //public async Task<IActionResult> Index()
        //{
        //    var result = await _galeryFileFacde.GetAll(_lang);
        //    return View(result);
        //}

        #region ::ADD::

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GaleryAddViewModel galeryAddViewModel)
        {
            var resultGalery = await _galeryFacade.Add(galeryAddViewModel);
            var resultImage = await galeryAddViewModel.files.SaveImageCollectionAsync(_env, "galery", _unitOfWork);
            if (resultGalery.IsDone && resultImage.Count > 0)
            {
                await GaleryFileSaveWithImageAndGalery(resultGalery, resultImage);
                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion ::ADD::

        #region ::UPDATE::

        public async Task<IActionResult> Update(int id)
        {
            var resultFindById = await _galeryFacade.FindUpdate(id);
            return View(resultFindById);
        }

        public async Task<IActionResult> Update(GaleryUpdateViewModel galeryUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
            }
            var result = await _galeryFacade.Update(galeryUpdateViewModel);
            if (result.IsDone)
            {
                return RedirectToAction("Index");
            }
            return View(galeryUpdateViewModel);
        }

        #endregion ::UPDATE::

        #region ::DELETE::

        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
            }
            var result = await _galeryFacade.Delete(id);
            if (result.IsDone)
            {
                return RedirectToAction("Index");
            }
            return View();
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
                    GaleryId = resultGalery.Data.Id
                };
                await _galeryFileFacde.Add(galeryFileAddViewModel);
            }
        }

        #endregion ::private::
    }
}