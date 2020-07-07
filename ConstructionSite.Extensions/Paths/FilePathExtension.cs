using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ConstructionSite.Extensions.Paths
{
    public static class FilePathExtension
    {
        public static string GetPath(this IFormFile file, string SubFolder)
        {
            string extension =file.GetFileName();
            string fileName = Path.Combine(SubFolder,extension);

            string path = Path.Combine("images", fileName);
            return path;
        }
        public static string GetFileName(this IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string result=  Guid.NewGuid().ToString() + extension;
            return result.ToString();
        }
    }
}