using ConstructionSite.DTO.AdminViewModels.SubService;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Services;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Services
{
    public class SubServiceFacde : ISubServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubServiceFacde(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public Task<RESULT<SubService>> Add(SubServiceAddModel subServiceAddModel)
        {
            var resultSubServiceAddModel = new SubService
            {
                NameAz = subServiceAddModel.NameAz,
                NameRu = subServiceAddModel.NameRu,
                NameEn = subServiceAddModel.NameEn,
                ContentAz = subServiceAddModel.ContentAz,
                ContentEn = subServiceAddModel.ContentEn,
                ContentRu = subServiceAddModel.ContentRu,
                ServiceId = subServiceAddModel.ServiceId,
            };

            var resultSubServiceAdd = _unitOfWork.SubServiceRepository.Add(resultSubServiceAddModel);
            return Task.FromResult(resultSubServiceAdd);
        }

        public List<SubServiceViewModel> GetAll(string _lang)
        {
            var resultSubService = _unitOfWork.SubServiceRepository.GetAll()
                .Select(x => new SubServiceViewModel
                {
                    Id = x.Id,
                    Content = x.FindContent(_lang),
                    Name = x.FindName(_lang),
                    ImageId = x.SubServiceImages.Select(x => x.ImageId).FirstOrDefault(),
                    ImagePath = x.SubServiceImages.Select(x => x.Image.Path).FirstOrDefault()
                })
                .ToList();
            return resultSubService;
        }

        public List<SelectListItem> GetServices(string _lang)
        {
            return _unitOfWork.ServiceRepository.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.FindName(_lang),
                    Value = x.Id.ToString()
                }).ToList();
        }

        public SubServiceUpdateViewModel GetForUpdate(int id)
        {
            var resultSubService = _unitOfWork.SubServiceRepository.GetAll()
                  .Select(x => new SubServiceUpdateViewModel
                  {
                      Id = x.Id,
                      ContentAz = x.ContentAz,
                      ContentEn = x.ContentEn,
                      ContentRu = x.ContentRu,
                      NameAz = x.NameAz,
                      NameEn = x.NameEn,
                      NameRu = x.NameRu,
                      ServiceId = x.ServiceId,
                      Images = x.SubServiceImages.Select(x => x.Image).ToList(),
                      ImageID = x.SubServiceImages.Select(x => x.ImageId).ToList(),
                  })
                  .SingleOrDefault(x => x.Id == id);
            return resultSubService;
        }

        public Task<RESULT<SubService>> Update(SubServiceUpdateViewModel subServiceUpdateViewModel)
        {
            var resultSubService = _unitOfWork.SubServiceRepository.Find(x => x.Id == subServiceUpdateViewModel.Id);
            resultSubService.NameAz = subServiceUpdateViewModel.NameAz;
            resultSubService.NameEn = subServiceUpdateViewModel.NameEn;
            resultSubService.NameRu = subServiceUpdateViewModel.NameRu;
            resultSubService.ContentAz = subServiceUpdateViewModel.ContentAz;
            resultSubService.ContentEn = subServiceUpdateViewModel.ContentEn;
            resultSubService.ContentRu = subServiceUpdateViewModel.ContentRu;
            resultSubService.ServiceId = subServiceUpdateViewModel.ServiceId;
            var result = _unitOfWork.SubServiceRepository.Update(resultSubService);
            return Task.FromResult(result);
        }

        public bool Delete(int id)
        {
            var data = _unitOfWork.SubServiceRepository.Find(x => x.Id == id);
            var imageId = _unitOfWork.SubServiceImageRepository.GetAll()
                  .Where(x => x.SubServiceId == data.Id)
                  .Select(x => x.ImageId).ToArray();
            _unitOfWork.SubServiceRepository.Delete(data);
            return _webHostEnvironment.Delete(imageId, "service", _unitOfWork);
        }
    }
}