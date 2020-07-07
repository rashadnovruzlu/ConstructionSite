using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Paths;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
   public static class ImageSaveExtension
    {
        public async static Task<int> SaveImage(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            if (file.IsImage())
            {

                string name = await file.SaveAsync(_env,subFolder);
                image.Title = file.GetFileName();
                image.Path = name;
                await _unitOfWork.imageRepository.AddAsync(image);
                _unitOfWork.Dispose();
            }
            return image.Id;
        }
        private static async Task<string> SaveAsync(this IFormFile file, IWebHostEnvironment _env, string subFolder)
        {

            string fileName = file.GetPath(subFolder);
            string path = Path.Combine(_env.WebRootPath, fileName);
            file.Create(_env.WebRootPath, subFolder);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/" + fileName;
        }
        private static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/x-png" ||
                   file.ContentType == "image/gif";
        }
    }
}
