using ConstructionSite.DTO.AdminViewModels.About;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Interfaces.Facade;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.About
{
    public class AboutFacade : IAboutFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public AboutFacade(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public IEnumerable<AboutViewModel> GetAll(string _lang)
        {
            var result = _unitOfWork.AboutImageRepository.GetAll()
         .Include(x => x.About)
          .Include(x => x.Image)
           .Select(x => new AboutViewModel
           {
               Id = x.Id,
               aboutID = x.About.Id,
               Tittle = x.About.FindTitle(_lang),
               Content = x.About.FindContent(_lang),
               imageId = x.Image.Id,
               Image = x.Image.Path
           })
             .AsQueryable();

            return result;
        }

        public async Task<bool> Insert(AboutAddViewModel aboutAddViewModel, IFormFile FileData)
        {
            bool isResult = false;
            AboutImage aboutImage = new AboutImage();
            Image image = new Image();
            var aboutAddViewModelResult = new ConstructionSite.Entity.Models.About
            {
                Id = aboutAddViewModel.Id,
                TittleAz = aboutAddViewModel.TittleAz,
                TittleRu = aboutAddViewModel.TittleRu,
                TittleEn = aboutAddViewModel.TittleEn,
                ContentAz = aboutAddViewModel.ContentAz,
                ContentEn = aboutAddViewModel.ContentEn,
                ContentRu = aboutAddViewModel.ContentRu
            };
            var aboutSaveResult = await _unitOfWork.AboutRepository.AddAsync(aboutAddViewModelResult);

            bool imageSaveResult = await FileData.SaveImageAsync(_env, "about", image, _unitOfWork);

            if (aboutSaveResult.IsDone && imageSaveResult)
            {
                aboutImage.ImageId = image.Id;
                aboutImage.AboutId = aboutAddViewModelResult.Id;

                var aboutImageResult = await _unitOfWork.AboutImageRepository.AddAsync(aboutImage);
                if (!aboutImageResult.IsDone)
                {
                    ImageExtensions.DeleteAsyc(_env, image, "about", _unitOfWork);
                    _unitOfWork.Rollback();
                }
                else
                {
                    isResult = true;
                }
            }
            _unitOfWork.Dispose();
            return isResult;
        }

        public async Task<bool> Update(AboutUpdateViewModel aboutUpdateViewModel, IFormFile file)
        {
            bool isResult = false;
            ConstructionSite.Entity.Models.About UpdateAbout = new ConstructionSite.Entity.Models.About
            {
                Id = aboutUpdateViewModel.aboutID,
                ContentAz = aboutUpdateViewModel.ContentAz,
                ContentEn = aboutUpdateViewModel.ContentEn,
                ContentRu = aboutUpdateViewModel.ContentRu,
                TittleAz = aboutUpdateViewModel.TittleAz,
                TittleEn = aboutUpdateViewModel.TittleEn,
                TittleRu = aboutUpdateViewModel.TittleRu,
            };
            var aboutResult = await _unitOfWork.AboutRepository.UpdateAsync(UpdateAbout);

            if (file != null && aboutResult.IsDone)
            {
                Image image = _unitOfWork.imageRepository.GetById(aboutUpdateViewModel.imageId);
                if (image != null)
                {
                    isResult = await file.UpdateAsyc(_env, image, "about", _unitOfWork);
                }


            }

            var updateAboutImage = new AboutImage
            {
                Id = aboutUpdateViewModel.Id,
                ImageId = aboutUpdateViewModel.imageId,
                AboutId = UpdateAbout.Id,
            };
            var AboutImageResult =
             await _unitOfWork.AboutImageRepository.UpdateAsync(updateAboutImage);
            isResult = aboutResult.IsDone;
            if (!AboutImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                isResult = false;
            }
            else
            {
                isResult = true;
            }
            _unitOfWork.Dispose();
            return isResult;
        }
    }
}