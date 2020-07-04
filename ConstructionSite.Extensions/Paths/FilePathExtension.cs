using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConstructionSite.Extensions.Paths
{
  public static  class FilePathExtension
    {
      public static string  GetPath (this IFormFile file, string SubFolder)
        {
            string extension = Path.GetExtension(file.FileName);
            string fileName = Path.Combine(SubFolder, Guid.NewGuid().ToString() + extension);


            string path= Path.Combine("images", fileName);
            return path;
        }
    }
}
