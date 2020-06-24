using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
  public static  class ImageExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/x-png" ||
                   file.ContentType == "image/gif";
        }

        public static async Task<string> SaveAsync(this IFormFile file, string root, string subFolder)
        {
            string fileName = Path.Combine(subFolder, Guid.NewGuid().ToString() + Path.GetFileName(file.FileName));

            string path = Path.Combine(root, "images", fileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
