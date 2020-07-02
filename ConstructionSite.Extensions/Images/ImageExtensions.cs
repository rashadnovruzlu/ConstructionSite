using ConstructionSite.Helpers.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class ImageExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/x-png" ||
                   file.ContentType == "image/gif";
        }

        public static async Task<string> SaveAsync(this IFormFile file, IWebHostEnvironment _env, string subFolder)
        {


          
                string fileName = Imager.GetImageSubFolder(subFolder, file.FileName);

                string path = Path.Combine(_env.WebRootPath, "images", fileName);

                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
           
        }
    }
}