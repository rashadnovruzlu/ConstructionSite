using ConstructionSite.DTO.AdminViewModels.About;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.About;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.About;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data = ConstructionSite.Entity.Models;

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

        public async Task<RESULT<data.About>> AddAsync(AboutAddViewModel aboutAddViewModel)
        {


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
            return await _unitOfWork.AboutRepository.AddAsync(aboutAddViewModelResult);





        }

        public async Task<RESULT<data.About>> Update(AboutUpdateViewModel aboutImageUpdateViewModel)
        {
            data.About about = new data.About
            {
                Id = aboutImageUpdateViewModel.Id,
                ContentAz = aboutImageUpdateViewModel.ContentAz,
                ContentEn = aboutImageUpdateViewModel.ContentEn,
                ContentRu = aboutImageUpdateViewModel.ContentRu,
                TittleAz = aboutImageUpdateViewModel.TittleAz,
                TittleEn = aboutImageUpdateViewModel.TittleEn,
                TittleRu = aboutImageUpdateViewModel.TittleRu
            };

            return await _unitOfWork.AboutRepository.UpdateAsync(about);

        }
        
        public async Task<List<Image>> FindImageByAboutID(int aboutID)
        {
            var resultImageUpdateViewModel = await _unitOfWork.AboutImageRepository.GetAll()
                      .Include(x => x.Image)
                      .Where(x => x.AboutId == aboutID)
                      .Select(x => new Image
                      {
                          Path = x.Image.Path,
                          Title = x.Image.Title,
                          VideoPath = x.Image.VideoPath,
                          Id = x.Image.Id
                      })
                      .ToListAsync();

            return resultImageUpdateViewModel;

        }


    }
}