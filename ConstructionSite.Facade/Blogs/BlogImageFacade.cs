using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.News;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<NewsViewModel> GetAll(string _lang)
        {
            var data = _unitOfWork.newsRepository.GetAll()
                .Select(x => new NewsViewModel
                {
                    Id = x.Id,
                    Content = x.FindContent(_lang),
                    Title = x.ContentAz,
                    Imagepath = x.NewsImages.Select(x => x.Image.Path).FirstOrDefault()
                }).ToList();

            return data;
        }
    }
}