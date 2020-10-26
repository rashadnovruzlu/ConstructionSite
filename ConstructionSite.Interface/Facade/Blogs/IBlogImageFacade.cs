using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Blogs
{
    public interface IBlogImageFacade
    {
        List<NewsViewModel> GetAll(string _lang);

        Task<RESULT<NewsImage>> Add(NewsImageAddViewModel newsImageAddViewModel);
    }
}
