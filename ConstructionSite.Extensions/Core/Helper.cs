using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ConstructionSite.Extensions.Core
{
    public static class Helper
    {
        public static string reNameFileName(IFormFile file)
        {
            string fileNameExtension = Path.GetExtension(file.FileName);
            string FileNameAfterReName = Guid.NewGuid().ToString() + fileNameExtension;
            return FileNameAfterReName;
        }
    }
}