using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Service
{
    public interface IServiceImageFacade
    {
        Task<List<ServiceImageViewModel>> GetAll(string _lang);
        Task<RESULT<ServiceImage>> Add(ServiceImageAddViewModel serviceImageAddViewModel);
        Task<RESULT<ServiceImage>> Delete(int id);
        Task<RESULT<ServiceImage>> Update(ServiceImageUpdateViewModel serviceImageUpdateViewModel);
    }
}
