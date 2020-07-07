﻿using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Paths;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class SaveFileExtension
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
        public static void DeleteImage(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
           var folder= file.GetPath(subFolder);
            var imageName=file.GetFileName();
            string _imageToBeDeleted = Path.Combine(_env.WebRootPath,folder);
            if (File.Exists(_imageToBeDeleted))
            {
                File.Delete(_imageToBeDeleted);
            }


        }
    }
}