using ConstructionSite.Extensions.Paths;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class ImageExtensions
    {
        public static async Task<string> SaveAsync(this IFormFile file, IWebHostEnvironment _env, string subFolder)
        {
            string fileName = file.GetPath(subFolder);
            string path = Path.Combine(_env.WebRootPath, fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public static async Task<string> SaveAsyncArray(this List<IFormFile> files, IWebHostEnvironment _env, string subFolder)
        {
            foreach (var formFile in files)
            {
                string fileName = null;

                string path = Path.Combine(_env.WebRootPath, "images", fileName);
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
                return fileName;
            }
            return null;
        }
    }
}