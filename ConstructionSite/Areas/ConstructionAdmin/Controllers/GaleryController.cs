using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class GaleryController : CoreController
    {
        #region ::FILDS::

        private readonly IGaleryFacade _galeryFacade;

        #endregion ::FILDS::

        #region ::CTOR::

        public GaleryController(IGaleryFacade galeryFacade)
        {
            _galeryFacade = galeryFacade;
        }

        #endregion ::CTOR::

        public IActionResult Index()
        {
            return View();
        }

        #region ::ADD::

        public IActionResult Add()
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
            var result = await _galeryFacade.Add(galeryAddViewModel);
            if (result)
            {
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