using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Helpers.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.Interface.Facade.Blogs
{
    public interface IBlogFacade
    {
        Task<RESULT<data.News>> Add(BlogAddViewModel blogAddViewModel);

        Task<bool> Update(BlogEditModel blogEditModel);

        List<BlogViewModel> GetAll(string _lang);

        bool Delete(int id);
    }
}