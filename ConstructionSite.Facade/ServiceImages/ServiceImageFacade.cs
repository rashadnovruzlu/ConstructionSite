using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Interfaces.Facade;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.ServiceImages
{
    public class ServiceImageFacade : IServiceImageFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceImageFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Add(ServiceImageAddViewModel serviceImageAddViewModel)
        {
            var resultserviceImageAddViewModel = await serviceImageAddViewModel.MappedAsync<ServiceImage>();
            await _unitOfWork.ServiceImageRepstory.AddAsync(resultserviceImageAddViewModel);
            return await CeckedTransaction();

        }

        public async Task<bool> Delete(int id)
        {
            var resultServiceImage = _unitOfWork.ServiceImageRepstory.FindAllAsync(x => x.Id == id);
            var resultServiceImageMapped = await resultServiceImage.MappedAsync<ServiceImage>();
            await _unitOfWork.ServiceImageRepstory.DeleteAsync(resultServiceImageMapped);
            return await CeckedTransaction();
        }

        public Task<bool> Update()
        {
            throw new NotImplementedException();
        }
        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
