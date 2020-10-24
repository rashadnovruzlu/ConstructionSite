using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Servics;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Services
{
    public class ServiceFacade : IServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RESULT<Service>> Add(ServiceAddViewModel serviceAddViewModel)
        {
            var resultData = await serviceAddViewModel.MappedAsync<Service>();
            return await _unitOfWork.ServiceRepository.AddAsync(resultData);
        }

       

        public Task<bool> Update()
        {
            throw new NotImplementedException();
        }
    }
}
