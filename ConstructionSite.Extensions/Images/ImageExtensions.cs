using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class ImageExtensions
    {
        private const string _IMAGE = "images";

        public async static Task<int> SaveImage(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            if (file.IsImage())
            {
                string name = await file.SaveAsync(_env, subFolder);
                image.Title = file.GetFileName();
                image.Path = name;
                await _unitOfWork.imageRepository.AddAsync(image);
            }
            return image.Id;
        }

        private static async Task<string> SaveAsync(this IFormFile file, IWebHostEnvironment _env, string subFolder)
        {
            if (file is null)
            {
                return string.Empty;
            }
            string ImageName = file.GetFileName();
            string filePath = Path.Combine(_env.WebRootPath, _IMAGE, subFolder, ImageName);
            string folderPath = Path.Combine(_env.WebRootPath, _IMAGE, subFolder);
            Create(folderPath);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string dbPaht = "/" + Path.Combine(_IMAGE, subFolder, ImageName);
            return dbPaht;
        }

        public static void Delete(this IFormFile file, IWebHostEnvironment _env, Image image, string subFolder)
        {
            string _imageToBeDeleted = Path.Combine(_env.WebRootPath, _IMAGE, subFolder, image.Title);

            if ((System.IO.File.Exists(_imageToBeDeleted)))
            {
                System.IO.File.Delete(_imageToBeDeleted);
            }
        }

        public static void Update(this IFormFile file, IWebHostEnvironment _env, Image image, string subFolder)
        {
            string FilePath = Path.Combine(_env.WebRootPath, _IMAGE, subFolder, image.Title);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        public async static void UpdateAsyc(this IFormFile file, IWebHostEnvironment _env, Image image, string subFolder, IUnitOfWork _unitOfWork)
        {
            if (file.IsImage())
            {
                string name = await file.SaveAsync(_env, subFolder);
                image.Title = file.GetFileName();
                image.Path = name;
                await _unitOfWork.imageRepository.UpdateAsync(image);
            }
        }

        private static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/x-png" ||
                   file.ContentType == "image/gif";
        }

        private static void Create(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static string GetFileName(this IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string result = Guid.NewGuid().ToString() + extension;
            return result.ToString();
        }
    }
}