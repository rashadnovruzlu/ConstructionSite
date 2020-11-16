using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class SliderController : CoreController
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISliderFacade _sliderFacade;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public SliderController(
                                IWebHostEnvironment webHostEnvironment,
                                IUnitOfWork unitOfWork,
                                ISliderFacade sliderFacade,
                                IHttpContextAccessor httpContextAccessor)
        {
            _sliderFacade = sliderFacade;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }

        public IActionResult Index()
        {
            var result = _sliderFacade.GetBackAll(_lang);
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SliderAddViewModel sliderAddViewModel)
        {
            var resultImagePath = await sliderAddViewModel.file.SaveImageForSlider(_webHostEnvironment, "Slider");
            if (!string.IsNullOrEmpty(resultImagePath))
            {
                sliderAddViewModel.ImagePath = resultImagePath;
                await _sliderFacade.Add(sliderAddViewModel);
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int Id)
        {
            var result = _sliderFacade.GetUpdate(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SliderUpdateViewModel sliderUpdateViewModel)
        {
            RESULT<Sliders> resultUpdateData = await UpdateAndDelete(sliderUpdateViewModel);
            if (resultUpdateData.IsDone)
            {
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var resultAfterDelete = _sliderFacade.Delete(id);
            if (resultAfterDelete)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #region ::PRIVITE::

        private async Task<RESULT<Sliders>> UpdateAndDelete(SliderUpdateViewModel sliderUpdateViewModel)
        {
            if (sliderUpdateViewModel.files != null)
            {
                if (!string.IsNullOrEmpty(sliderUpdateViewModel.pathImage))
                {
                    _webHostEnvironment.DeleteFormHardDiskSlider("Slider", sliderUpdateViewModel.pathImage);
                }
                var result = await sliderUpdateViewModel.files.SaveImageForSlider(_webHostEnvironment, "Slider");

                sliderUpdateViewModel.pathImage = result;
            }

            await _unitOfWork.CommitAsync();
            var resultUpdateData = await _sliderFacade.Update(sliderUpdateViewModel);
            return resultUpdateData;
        }

        #endregion ::PRIVITE::
    }
}