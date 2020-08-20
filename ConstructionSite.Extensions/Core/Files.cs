using Microsoft.AspNetCore.Http;
using System.IO;

namespace ConstructionSite.Extensions.Core
{
    public static class Files
    {
        public static void deleteFileFormHardDisk(string PathForDeleteFile)
        {
            if (File.Exists(PathForDeleteFile))
            {
                File.Delete(PathForDeleteFile);
            }
        }

        public async static void saveImageForDisk(this IFormFile file, string path)
        {
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}