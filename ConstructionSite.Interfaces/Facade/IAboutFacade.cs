using ConstructionSite.DTO.AdminViewModels.About;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interfaces.Facade
{
    public interface IAboutFacade
    {

        IEnumerable<AboutViewModel> GetAll(string _lang);
        Task<bool> Insert(AboutAddViewModel aboutAddViewModel, IFormFile FileData);
        Task<bool> Update(AboutUpdateViewModel aboutUpdateViewModel, IFormFile file);
    }
}
