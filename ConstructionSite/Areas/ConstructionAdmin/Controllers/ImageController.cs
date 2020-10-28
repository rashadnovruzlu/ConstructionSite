using ConstructionSite.Extensions.Images;
using ConstructionSite.Interface.Facade.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class ImageController : CoreController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageFacade _imageFacade;
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IImageFacade imageFacade,
                               IUnitOfWork unitOfWork,
                               IWebHostEnvironment webHostEnvironment)
        {
            _imageFacade = imageFacade;
            _unitOfWork = unitOfWork;
            _imageFacade = imageFacade;
        }

        [HttpPost]
        public async Task<IActionResult> blogUpdate(IFormFile file, int id)
        {
            var result = await _unitOfWork.imageRepository.FindAsync(x => x.Id == id);
            if (result != null)
            {
                await file.UpdateAsyc(_webHostEnvironment, result, "blog", _unitOfWork);
                await _unitOfWork.CommitAsync();
            }
            return View();
        }

        public async Task<IActionResult> aboutUpdate(int id)
        {
            return View();
        }
    }
}