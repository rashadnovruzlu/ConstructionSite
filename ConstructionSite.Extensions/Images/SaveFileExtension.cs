using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class SaveFileExtension
    {
        public async static Task<int> SaveImage(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            if (file.IsImage())
            {
                string name = await file.SaveAsync(_env, subFolder);
                image.Title = name;
                image.Path = name;
                await _unitOfWork.imageRepository.AddAsync(image);
            }
            return image.Id;
        }
    }
}