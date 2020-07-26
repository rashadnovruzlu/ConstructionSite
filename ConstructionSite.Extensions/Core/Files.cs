using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConstructionSite.Extensions.Core
{
   public static  class Files
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
