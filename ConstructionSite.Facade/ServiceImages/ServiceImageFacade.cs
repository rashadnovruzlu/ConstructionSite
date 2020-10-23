using ConstructionSite.Entity.Models;
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

        public Task<List<ServiceImageViewModel>> GetAll(string _lang)
        {
          return   _unitOfWork.ServiceImageRepstory.GetAll()
                .Include(x => x.Image)
                .Include(x => x.Service)
                .Select(x => new ServiceImageViewModel
                {
                    Id=x.Id,
                    Content=x.Service.FindContent(_lang),
                    Name=x.Service.FindName(_lang),
                    Title=x.Service.FindTitle(_lang),
                    Path=x.Image.Path,
                    TitlePhoto=x.Image.Title,
                    VideoPath=x.Image.VideoPath

                })
                .ToListAsync();
        }

        public async Task<bool> Update(ServiceImageUpdateViewModel serviceImageUpdateViewModel)
        {
            var resultserviceImageUpdateViewModel = await _unitOfWork.ServiceImageRepstory.FindAsync(x => x.Id == serviceImageUpdateViewModel.Id);
            var serviceImageUpdateViewModelMapped = await resultserviceImageUpdateViewModel.MappedAsync<ServiceImage>();
            await _unitOfWork.ServiceImageRepstory.DeleteAsync(serviceImageUpdateViewModelMapped);
            return await CeckedTransaction();
        }
        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
