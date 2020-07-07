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

            string reName=file.GetFileName();
            string fileName =GetPath(reName,subFolder);
            string DirectoryPath=GetPath(subFolder);
            string path = Path.Combine(_env.WebRootPath, fileName);
            Create(_env.WebRootPath,DirectoryPath);
           
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/" + fileName;
        }
        public static void Delete(this IFormFile file, IWebHostEnvironment _env, Image image,string subFolder,IUnitOfWork unitOfWork )
        {
           
            var imagePath=Path.Combine(_env.WebRootPath, "images", subFolder,image.Title);

           
            if (File.Exists(imagePath)){
                File.Delete(imagePath);
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
        private static void Create(string rootPath, string folderName)
        {
            var FolderPath = Path.Combine(rootPath, "images", folderName);

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(FolderPath);
            }

        }
        public static string GetPath(string SubFolder)
        {
          return  Path.Combine("images", SubFolder);
        }
        public static string GetPath(string name, string SubFolder)
        {
            
            string fileName = Path.Combine(SubFolder,name);

            string path = Path.Combine("images", fileName);
            return path;
        }
        private static string GetFileName(this IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string result=  Guid.NewGuid().ToString() + extension;
            return result.ToString();
        }
    }
}