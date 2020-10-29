using ConstructionSite.DTO.AdminViewModels.Blog;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Blogs
{
    public interface IBlogQueryFacade
    {
        Task<BlogEditModel> GetForUpdate(int id);
    }
}