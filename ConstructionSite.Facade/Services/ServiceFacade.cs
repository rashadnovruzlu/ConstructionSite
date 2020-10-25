using data = ConstructionSite.DTO.AdminViewModels.Service;
using front = ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Servics;
using ConstructionSite.Repository.Abstract;
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
        public async Task<RESULT<front.ServiceDeatilyViewModel>> GetDeaiy(int id, string _lang)
        {
            //var result = await _unitOfWork.ServiceRepository.FindAsync(x => x.Id == id);
            //front.ServiceDeatilyViewModel serviceDeatilyViewModel = new Service
            //{
            //    Id = result.Id,


            //};
            return null;

        }

        public Task<bool> Update()
        {
            throw new NotImplementedException();
        }
    }
}
