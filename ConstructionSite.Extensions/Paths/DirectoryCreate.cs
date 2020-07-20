using Microsoft.AspNetCore.Http;
using System.IO;

namespace ConstructionSite.Extensions.Paths
{
    public static class DirectoryCreate
    {
       public static void Create(this IFormFile file ,string rootPath,string folderName)
        {
            var FolderPath = Path.Combine(rootPath,"images",folderName);
            
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(FolderPath);
            }
            
        }
    }
}
