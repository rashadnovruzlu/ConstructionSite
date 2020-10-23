using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Interface.Facade.Service;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> Add(ServiceAddViewModel serviceAddViewModel)
        {
            var serviceResult = await serviceAddViewModel.MappedAsync<Service>();
            var result = await _unitOfWork.ServiceRepository.AddAsync(serviceResult);
            return result.IsDone;

        }

        public async Task<List<ServiceImageViewModel>> GetAll(string _lang)
        {
            var resultServiceImage = await _unitOfWork.ServiceImageRepstory.GetAll()
                   .Include(x => x.Image)
                   .Include(x => x.Service)
                   .Select(x => new ServiceImageViewModel
                   {
                       Id = x.Id,
                       Content = x.Service.FindContent(_lang),
                       Name = x.Service.FindName(_lang),
                       Path = x.Image.Path,
                       Title = x.Service.FindTitle(_lang),
                       TitlePhoto = x.Image.Title,
                       VideoPath = x.Image.VideoPath

                   })
                   .ToListAsync();
            return await Task.FromResult(resultServiceImage);

        }

        public Task<bool> Update()
        {
            throw new NotImplementedException();
        }
    }
}
