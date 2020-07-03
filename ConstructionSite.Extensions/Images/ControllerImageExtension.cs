using ConstructionSite.Entity.Models;
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
  public static   class ControllerImageExtension
    {
        public async static Task<int> SaveImage(this IFormFile file, IWebHostEnvironment _env, string subFolder,Image image, IUnitOfWork _unitOfWork)
        {
            if (file.IsImage())
            {
                string name = await file.SaveAsync(_env, subFolder);
                image.Title = name;
                image.Path = Path.Combine(_env.WebRootPath, "images", name);
                return await _unitOfWork.imageRepository.AddAsync(image);

            }
            return 0;

        }
    }
}
