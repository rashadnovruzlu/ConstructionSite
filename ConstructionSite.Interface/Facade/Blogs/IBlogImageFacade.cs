using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.News;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Blogs
{
    public interface IBlogImageFacade
    {
        Task<RESULT<NewsImage>> Add(NewsImageAddViewModel newsImageAddViewModel);
    }
}
