using ConstructionSite.DTO.AdminViewModels.About;
using data = ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.ViwModel.AdminViewModels.About;

namespace ConstructionSite.Interface.Facade.About
{
    public interface IAboutFacade
    {

        IEnumerable<AboutViewModel> GetAll(string _lang);
        Task<RESULT<data.About>> AddAsync(AboutAddViewModel aboutAddViewModel);
        Task<RESULT<data.About>> Update(AboutUpdateViewModel aboutImageUpdateViewModel);
        Task<List<Image>> FindImageByAboutID(int id);
        AboutUpdateViewModel GetForUpdate(int id);
        bool Delete(int id);

    }
}
