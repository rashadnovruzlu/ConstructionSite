using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Interface.Facade.Services;
using ConstructionSite.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Services
{
    public class ServiceQueryFacade : IServiceQueryFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceQueryFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ServiceViewModel>> GetAll(string _lang)
        {
            var result = await _unitOfWork.ServiceImageRepstory.GetAll()
                .Include(x => x.Image)
                .Include(x => x.Service)
                .Select(x => new ServiceViewModel
                {
                    Id = x.Id,
                    image = x.Image.Path,
                    Name = x.Service.FindName(_lang),
                    Tittle = x.Service.FindContent(_lang)

                })
                 .ToListAsync();

            return result;
        }


    }
}
