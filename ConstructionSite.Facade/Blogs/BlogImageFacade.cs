using ConstructionSite.Entity.Models;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.News;
using ConstructionSite.Extensions.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConstructionSite.Helpers.Core;

namespace ConstructionSite.Facade.Blogs
{
    public class BlogImageFacade : IBlogImageFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogImageFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RESULT<NewsImage>> Add(NewsImageAddViewModel newsImageAddViewModel)
        {
            var result = await newsImageAddViewModel.MappedAsync<NewsImage>();
            return await _unitOfWork.newsImageRepository.AddAsync(result);
        }
    }
}
