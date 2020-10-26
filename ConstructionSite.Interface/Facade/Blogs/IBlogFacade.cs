using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.Helpers.Core;
using System.Threading.Tasks;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.Interface.Facade.Blogs
{
    public interface IBlogFacade
    {
        Task<RESULT<data.News>> Add(BlogAddViewModel blogAddViewModel);
        Task<RESULT<data.News>> Update(BlogEditModel blogEditModel);

    }
}
