using ConstructionSite.DTO.AdminViewModels.SubService;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Services;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Services
{
    public class SubServiceFacde : ISubServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubServiceFacde(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
