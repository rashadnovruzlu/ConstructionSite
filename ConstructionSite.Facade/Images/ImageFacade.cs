using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Images;
using ConstructionSite.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Images
{
    public class ImageFacade : IImageFacade
    {
        private readonly IUnitOfWork _unitOfwork;
        public ImageFacade(IUnitOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;

        }



        public async Task<RESULT<Image>> Update(Image image)
        {
            return await _unitOfwork.imageRepository.UpdateAsync(image);
        }
    }
}
