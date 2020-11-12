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
            var result = _sliderFacade.GetUpdate(id);
            return View(result);
        }
        public async Task<IActionResult> Update(SliderUpdateViewModel sliderUpdateViewModel)
        {
            if (sliderUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data not exists");
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
                return RedirectToAction("Index");
            }
            try
            {

                if (sliderUpdateViewModel.files != null && sliderUpdateViewModel.ImageID != null)
                {
                    try
                    {
                        for (int i = 0; i < sliderUpdateViewModel.ImageID.Count; i++)
                        {
                            var image = _unitOfWork.imageRepository.Find(x => x.Id == sliderUpdateViewModel.ImageID[i]);
                            await sliderUpdateViewModel.files[i].UpdateAsyc(_webHostEnvironment, image, "Slider", _unitOfWork);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (sliderUpdateViewModel.files != null)
                {
                    try
                    {
                        var emptyImage = _unitOfWork.ServiceRepository.Find(x => x.Id == sliderUpdateViewModel.Id);

                        var imagesid = await sliderUpdateViewModel.files.SaveImageCollectionAsync(_webHostEnvironment, "", _unitOfWork);
                        foreach (var item in imagesid)
                        {
                            var resultData = new ServiceImage
                            {
                                ServiceId = emptyImage.Id,
                                ImageId = item
                            };
                            await _unitOfWork.ServiceImageRepstory.AddAsync(resultData);
                        }
                        await _unitOfWork.CommitAsync();
                    }
                    catch
                    {
                    }
                }
                var resultAbout = await _sliderFacade.Update(sliderUpdateViewModel);
                if (resultAbout.IsDone)
                {
                    await _unitOfWork.CommitAsync();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
            }
            return View(sliderUpdateViewModel);
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
