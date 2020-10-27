using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Blogs
{
    public class BlogFacade : IBlogFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogFacade(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<bool> Update(BlogEditModel blogEditModel)
        {
            bool isSuccess = false;
            var resultBlogEditModel = await _unitOfWork.newsRepository.FindAsync(x => x.Id == blogEditModel.Id);
            resultBlogEditModel.TittleAz = blogEditModel.TittleAz;
            resultBlogEditModel.TittleEn = blogEditModel.TittleEn;
            resultBlogEditModel.TittleRu = blogEditModel.TittleRu;
            resultBlogEditModel.ContentAz = blogEditModel.ContentAz;
            resultBlogEditModel.ContentEn = blogEditModel.ContentEn;
            resultBlogEditModel.ContentRu = blogEditModel.ContentRu;
            var isResult = await _unitOfWork.newsRepository.UpdateAsync(resultBlogEditModel);
            if (isResult.IsDone)
            {
                if (await _unitOfWork.CommitAsync())
                {
                    isSuccess = true;
                }
            }
            return isSuccess;
        }
        public bool Delete(int id)
        {
            var data = _unitOfWork.newsRepository.Find(x => x.Id == id);
            var imageId = _unitOfWork.newsImageRepository.GetAll()
                  .Where(x => x.NewsId == data.Id)
                  .Select(x => x.ImageId).ToArray();
            _unitOfWork.newsRepository.Delete(data);
            return _webHostEnvironment.Delete(imageId, "blog", _unitOfWork);


        }
    }
}