using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConstructionSite.Helpers.Images
{
   public class PathImage
    {
        public static string GetPath(IFormFile file,string SubFolder)
        {
            string extension = Path.GetExtension(file.FileName);
            string fileName = Path.Combine(SubFolder, Guid.NewGuid().ToString() + extension);

            string path = Path.Combine("images", fileName);
            return path;
        }
    }
}
