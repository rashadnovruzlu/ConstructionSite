using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderRepostory _sliderRepostory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        public SliderController(ISliderRepostory sliderRepostory,
                                IWebHostEnvironment webHostEnvironment,
                                IUnitOfWork unitOfWork)
        {
            _sliderRepostory = sliderRepostory;
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
            await sliderAddViewModel.file.SaveImageCollectionAsync(_webHostEnvironment, "Slider", _unitOfWork);

            _unitOfWork.SliderRepostory.Add()
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
