using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class SliderController : CoreController
    {
        private readonly ISliderFacade _sliderFacade;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        public SliderController(
                                IWebHostEnvironment webHostEnvironment,
                                IUnitOfWork unitOfWork,
                                ISliderFacade sliderFacade)
        {
            _sliderFacade = sliderFacade;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SliderAddViewModel sliderAddViewModel)
        {


            var resultSlider = await _sliderFacade.Add(sliderAddViewModel);
            var resultImage = await sliderAddViewModel.file.SaveImageCollectionAsync(_webHostEnvironment, "Slider", _unitOfWork);
            try
            {
                if (resultSlider.IsDone && resultImage.Count > 0)
                {
                    foreach (var item in resultImage)
                    {
                        var result = new SliderImage
                        {
                            ImageId = item,
                            SlidersId = resultSlider.Data.Id
                        };
                        await _unitOfWork.SliderImageRepstory.AddAsync(result);
                    }
                    if (await _unitOfWork.CommitAsync())
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _unitOfWork.Rollback();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {


            }
            return View();
        }
        public IActionResult Update(int id)
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
