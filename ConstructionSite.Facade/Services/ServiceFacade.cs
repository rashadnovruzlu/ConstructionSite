using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Servics;
using ConstructionSite.Repository.Abstract;
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

        public ServiceFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT<Service>> Add(data.ServiceAddViewModel serviceAddViewModel)
        {
            var resultData = await serviceAddViewModel.MappedAsync<Service>();
            return await _unitOfWork.ServiceRepository.AddAsync(resultData);
        }

        public Task<List<front.ServiceViewModel>> GetAll(string _lang)
        {
            var result = _unitOfWork.ServiceImageRepstory.GetAll()
                 .Include(x => x.Service)
                 .Include(x => x.Image)
                 .Select(x => new front.ServiceViewModel
                 {
                     Id = x.Id,
                     Name = x.Service.FindName(_lang),
                     Tittle = x.Service.FindName(_lang),
                     image = x.Service.ServiceImages.Select(x => x.Image.Path).FirstOrDefault()
                 })

                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return result;
        }


        //public async Task<RESULT<front.ServiceDeatilyViewModel>> GetDeaiy(int id, string _lang)
        //{
        //    //var result = await _unitOfWork.ServiceRepository.FindAsync(x => x.Id == id);
        //    //front.ServiceDeatilyViewModel serviceDeatilyViewModel = new Service
        //    //{
        //    //    Id = result.Id,

        //    //};
        //    return null;
        //}

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
            var resultService = _unitOfWork.ServiceRepository.Find(x => x.Id == id);
            var resultSubservice = _unitOfWork.SubServiceRepository.GetAll()
                .Where(x => x.ServiceId == id)
                .AsEnumerable()

                .Select(x => x.SubServiceImages.Select(x => x.Image));
            return true;
            //  _unitOfWork.imageRepository.DeleteRange(resultSubservice);




        }

        Task<List<ServiceViewModel>> IServiceFacade.GetAll(string _lang)
        {
            throw new System.NotImplementedException();
        }
    }
}