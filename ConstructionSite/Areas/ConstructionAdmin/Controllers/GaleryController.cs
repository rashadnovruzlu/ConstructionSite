using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class GaleryController : CoreController
    {
        #region ::FILDS::
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGaleryFacade _galeryFacade;
        private readonly IGaleryFileFacde _galeryFileFacde;

        #endregion ::FILDS::

        #region ::CTOR::

        public GaleryController(IGaleryFacade galeryFacade, IGaleryFileFacde galeryFileFacde)
        {
            _galeryFacade = galeryFacade;
            _galeryFileFacde = galeryFileFacde;
            _lang = _httpContextAccessor.GetLanguages();
        }

        #endregion ::CTOR::

        public async Task<IActionResult> Index()
        {


            var result = await _galeryFileFacde.GetAll(_lang);
            return View(result);
        }

        #region ::ADD::

        public async Task<IActionResult> Add(GaleryFileAddViewModel galeryFileAddViewModel)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GaleryAddViewModel galeryAddViewModel)
        {
            if (!ModelState.IsValid)
            {

            }
            if (galeryAddViewModel == null)
            {

            }
            var isResult = await _galeryFacade.Add(galeryAddViewModel);
            if (isResult)
            {
                return RedirectToAction("Index");

            }

            return View(galeryAddViewModel);
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
            if (result)
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
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion ::DELETE::
    }
}