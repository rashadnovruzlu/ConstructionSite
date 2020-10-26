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
using System.Linq;
using ConstructionSite.DTO.AdminViewModels.News;
using Microsoft.EntityFrameworkCore;

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

        public  List<NewsViewModel> GetAll(string _lang)
        {
            return  _unitOfWork.newsImageRepository.GetAll()
                   .Include(x => x.News)
                   .Include(x => x.Image)
                   .Select(x => new NewsViewModel
                   {
                       Id = x.Id,
                       Content = x.News.FindContent(_lang),
                       Title = x.News.FindTitle(_lang),
                       Imagepath = x.Image.Path
                   }).ToList();

        }
    }
}
