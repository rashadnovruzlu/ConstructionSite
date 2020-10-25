using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Blogs
{
    public class BlogFacade : IBlogFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RESULT<News>> Add(BlogAddViewModel  blogAddViewModel)
        {
            var result = await blogAddViewModel.MappedAsync<News>();
            return await _unitOfWork.newsRepository.AddAsync(result);
        }
    }
}
