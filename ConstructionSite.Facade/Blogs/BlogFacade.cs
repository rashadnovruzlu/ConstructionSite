using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<RESULT<News>> Add(BlogAddViewModel blogAddViewModel)
        {
            var result = await blogAddViewModel.MappedAsync<News>();
            return await _unitOfWork.newsRepository.AddAsync(result);
        }

        public List<BlogViewModel> GetAll(string _lang)
        {
            var resultBlogViewModel = _unitOfWork.newsRepository.GetAll()
              .Select(x => new BlogViewModel
              {
                  Id = x.Id,
                  Title = x.FindTitle(_lang),
                  Content = x.FindContent(_lang),

                  Imagepath = x.NewsImages.Select(x => x.Image.Path).First(),
                  CreateDate = x.CreateDate,
              }).OrderByDescending(x => x.Id)
              .ToList();
            return resultBlogViewModel;
        }

        public async Task<RESULT<News>> Update(BlogEditModel blogEditModel)
        {
            var resultBlogEditModel = await _unitOfWork.newsRepository.FindAsync(x => x.Id == blogEditModel.Id);
            resultBlogEditModel.TittleAz = blogEditModel.TittleAz;
            resultBlogEditModel.TittleEn = blogEditModel.TittleEn;
            resultBlogEditModel.TittleRu = blogEditModel.TittleRu;
            resultBlogEditModel.ContentAz = blogEditModel.ContentAz;
            resultBlogEditModel.ContentEn = blogEditModel.ContentEn;
            resultBlogEditModel.ContentRu = blogEditModel.ContentRu;
            return await _unitOfWork.newsRepository.UpdateAsync(resultBlogEditModel);
        }
    }
}