using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Blogs
{
    public class BlogQueryFacade : IBlogQueryFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogQueryFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<BlogEditModel> GetForUpdate(int id)
        {
            var resultBlogEditModel = _unitOfWork.newsRepository.GetAll()
                                      .Select(x => new BlogEditModel
                                      {
                                          Id = x.Id,
                                          TittleAz = x.TittleAz,
                                          TittleEn = x.TittleEn,
                                          TittleRu = x.TittleRu,
                                          ContentAz = x.ContentAz,
                                          ContentEn = x.ContentEn,
                                          ContentRu = x.ContentRu,
                                          Images = x.NewsImages.Select(x => x.Image).ToList()

                                      }).SingleOrDefault(x => x.Id == id);
            return Task.FromResult(resultBlogEditModel);
        }
    }
}
