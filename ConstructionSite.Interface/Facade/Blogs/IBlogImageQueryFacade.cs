using ConstructionSite.DTO.AdminViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Blogs
{
   public interface IBlogImageQueryFacade
    {
        Task<BlogEditModel> GetForUpdate();
    }

}
