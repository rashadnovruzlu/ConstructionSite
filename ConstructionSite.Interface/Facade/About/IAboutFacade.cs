using ConstructionSite.DTO.AdminViewModels.About;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.About
{
    public interface IAboutFacade
    {

        IEnumerable<AboutViewModel> GetAll(string _lang);
        Task<bool> Insert(AboutAddViewModel aboutAddViewModel, IFormFile FileData);
        Task<bool> Update(AboutUpdateViewModel aboutUpdateViewModel, IFormFile file);
    }
}
