using ConstructionSite.DTO.AdminViewModels.About;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutFacade(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<AboutViewModel> GetAll(string _lang)
        {
            return _unitOfWork.AboutRepository.GetAll()
                    .Select(x => new AboutViewModel
                    {
                        Id = x.Id,
                        Content = x.FindContent(_lang),
                        Tittle = x.FindTitle(_lang),
                        Image = x.AboutImages.Select(x => x.Image.Path).FirstOrDefault()

                    })
                    .ToList();


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

            var result = await _unitOfWork.AboutRepository.FindAsync(x => x.Id == aboutImageUpdateViewModel.Id);

            result.ContentAz = aboutImageUpdateViewModel.ContentAz;
            result.ContentEn = aboutImageUpdateViewModel.ContentEn;
            result.ContentEn = aboutImageUpdateViewModel.ContentEn;
            result.TittleAz = aboutImageUpdateViewModel.TittleAz;
            result.TittleEn = aboutImageUpdateViewModel.TittleEn;
            result.TittleRu = aboutImageUpdateViewModel.TittleRu;
            var resultAbout = await _unitOfWork.AboutRepository.UpdateAsync(result);

            return resultAbout;



        }
        public AboutUpdateViewModel GetForUpdate(int id)
        {
            var result = _unitOfWork.AboutRepository.GetAll()
                  .Select(x => new AboutUpdateViewModel
                  {
                      Id = x.Id,
                      ContentAz = x.ContentAz,
                      ContentEn = x.ContentEn,
                      ContentRu = x.ContentRu,
                      TittleAz = x.TittleAz,
                      TittleEn = x.TittleEn,
                      TittleRu = x.TittleRu,
                      Images = x.AboutImages.Select(x => x.Image).ToList(),

                  })
                  .SingleOrDefault(x => x.Id == id);

            return result;

        }
        public bool Delete(int id)
        {
            var data = _unitOfWork.AboutRepository.Find(x => x.Id == id);
            var imageId = _unitOfWork.AboutImageRepository.GetAll()
                  .Where(x => x.AboutId == data.Id)
                  .Select(x => x.ImageId).ToArray();
            _unitOfWork.AboutRepository.Delete(data);
            return _webHostEnvironment.Delete(imageId, "blog", _unitOfWork);
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