using ConstructionSite.DTO.AdminViewModels.Blog;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Blogs
{
    public interface IBlogImageQueryFacade
    {
        Task<BlogEditModel> GetForUpdate();
    }
}