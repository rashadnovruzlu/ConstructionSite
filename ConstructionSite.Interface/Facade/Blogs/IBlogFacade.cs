using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.Helpers.Core;
using System.Threading.Tasks;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.Interface.Facade.Blogs
{
    public interface IBlogFacade
    {
        public Task<RESULT<data.News>> Add(BlogAddViewModel blogAddViewModel);
    }
}
