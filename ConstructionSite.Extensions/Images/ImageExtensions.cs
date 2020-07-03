using ConstructionSite.Helpers.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class ImageExtensions
    {
       

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
        //public static async Task<string> SaveAsyncWith(this List<IFormFile> files)
        //{
        //    //string fileName = Imager.GetImageSubFolder(subFolder, file.FileName);
         
        //    string path = Path.Combine(_env.WebRootPath, "images", fileName);
        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }
        //}
    }
}