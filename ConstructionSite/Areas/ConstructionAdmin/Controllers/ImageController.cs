using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Interface.Facade.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageFacade _imageFacade;
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IImageFacade imageFacade,
                               IUnitOfWork unitOfWork)
        {
            _imageFacade = imageFacade;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> blogUpdate(IFormFile file, int id)
        {
            var result = await _unitOfWork.imageRepository.FindAsync(x => x.Id == id);
            if (result != null)
            {
               
            }
            return View();
        }

    }
}
