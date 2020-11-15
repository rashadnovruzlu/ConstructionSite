using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
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
            var result = _sliderFacade.GetAll(_lang);
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
            var resultImagePath = _unitOfWork.SliderRepostory.Find(x => x.Id == sliderUpdateViewModel.Id).ImagePath;
            _webHostEnvironment.DeleteSlider("Slider", resultImagePath);

            await _sliderFacade.Update(sliderUpdateViewModel);
            return View();
        }
        public IActionResult Delete(int id)
        {
            var imagePathResult = _unitOfWork.SliderRepostory.Find(x => x.Id == id).ImagePath;

            _sliderFacade.Delete(id);
            if (_unitOfWork.Commit() > 0)
            {
                _webHostEnvironment.DeleteSlider("Slider", imagePathResult);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
