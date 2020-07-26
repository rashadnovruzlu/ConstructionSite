using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConstructionSite.Extensions.Core
{
   public static class Paths
    {
        public static string createfilePathSaveHardDisk(IWebHostEnvironment _env, string subFolder, string FileNameAfterReName, string _IMAGE)
        {
            string result = Path.Combine(_env.WebRootPath, _IMAGE, subFolder, FileNameAfterReName);
            return result;
        }
        public static string createFilePathSaveDataBase(string subFolder, string FileNameAfterReName,string _IMAGE)
        {
            return "/" + Path.Combine(_IMAGE, subFolder, FileNameAfterReName);
        }
    }
}
