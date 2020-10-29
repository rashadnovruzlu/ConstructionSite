using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Servics;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using data = ConstructionSite.DTO.AdminViewModels.Service;

using front = ConstructionSite.DTO.FrontViewModels.Service;

namespace ConstructionSite.Facade.Services
{
    public class ServiceFacade : IServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ServiceFacade(IUnitOfWork unitOfWork,
            IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<RESULT<Service>> Add(data.ServiceAddViewModel serviceAddViewModel)
        {
            var resultData = await serviceAddViewModel.MappedAsync<Service>();
            return await _unitOfWork.ServiceRepository.AddAsync(resultData);
        }

        public List<data.ServiceViewModel> GetAll(string _lang)
        {
            var resultServiceViewModel = _unitOfWork.ServiceRepository.GetAll()
                  .Select(x => new ServiceViewModel
                  {
                      Id = x.Id,
                      Name = x.FindName(_lang),
                      Tittle = x.FindTitle(_lang),
                      Image = x.ServiceImages.Select(x => x.Image.Path).FirstOrDefault()

                  })
                  .ToList();
            return resultServiceViewModel;
        }

        public async Task<RESULT<front.ServiceDeatilyViewModel>> GetDeaiy(int id, string _lang)
        {
            //var result = await _unitOfWork.ServiceRepository.FindAsync(x => x.Id == id);
            //front.ServiceDeatilyViewModel serviceDeatilyViewModel = new Service
            //{
            //    Id = result.Id,

            //};
            return null;
        }
        public ServiceUpdateViewModel GetForUpdate(int id)
        {
            var resultService = _unitOfWork.ServiceRepository.GetAll()
                 .Select(x => new ServiceUpdateViewModel
                 {
                     NameAz = x.NameAz,
                     NameEn = x.NameEn,
                     NameRu = x.NameRu,
                     ContentAz = x.ContentAz,
                     ContentEn = x.ContentEn,
                     ContentRu = x.ContentRu,
                     TittleAz = x.TitleAz,
                     TittleEn = x.TitleEn,
                     TittleRu = x.TitleRu
                 }
                     )
                 .SingleOrDefault(x => x.id == id);
            return resultService;
        }

        public async Task<RESULT<Service>> Update(ServiceUpdateViewModel serviceUpdateViewModel)
        {
            var result = await _unitOfWork.ServiceRepository.FindAsync(x => x.Id == serviceUpdateViewModel.id);
            result.TitleAz = serviceUpdateViewModel.TittleAz;
            result.TitleEn = serviceUpdateViewModel.TittleEn;
            result.TitleRu = serviceUpdateViewModel.TittleRu;
            result.NameAz = serviceUpdateViewModel.NameAz;
            result.NameEn = serviceUpdateViewModel.NameEn;
            result.NameRu = serviceUpdateViewModel.NameRu;
            result.ContentAz = serviceUpdateViewModel.ContentAz;
            result.ContentEn = serviceUpdateViewModel.ContentEn;
            result.ContentRu = serviceUpdateViewModel.ContentRu;
            return await _unitOfWork.ServiceRepository.UpdateAsync(result);
        }

        public bool Delete(int id)
        {
            var subImage = _unitOfWork.SubServiceImageRepository.GetAll()
                  .Where(x => x.SubService.ServiceId == id)
                  .Select(x => x.Image)
                  .ToArray();
            _env.Delete(subImage, "service", _unitOfWork);
            var service = _unitOfWork.ServiceRepository.Find(x => x.Id == id);
            var serviceImage = _unitOfWork.ServiceImageRepstory.GetAll()
                 .Where(x => x.ServiceId == id)
                 .Select(x => x.Image)
                 .ToArray();
            _env.Delete(serviceImage, "service", _unitOfWork);


            _unitOfWork.ServiceRepository.Delete(service);
            if (_unitOfWork.Commit() > 0)
            {
                return true;
            }
            else
            {
                _unitOfWork.Rollback();
                return false;
            }






        }


    }
}