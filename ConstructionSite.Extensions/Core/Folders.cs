using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ConstructionSite.Extensions.Core
{
    public static class Folders
    {
        public static bool createFolder(IWebHostEnvironment _env, string subFolder, string _IMAGE)
        {
            string folderPath = Path.Combine(_env.WebRootPath, _IMAGE, subFolder);
            bool isResult = false;
            if (!Directory.Exists(folderPath))
            {
                isResult = true;
                Directory.CreateDirectory(folderPath);
            }
            return isResult;
        }
    }
}