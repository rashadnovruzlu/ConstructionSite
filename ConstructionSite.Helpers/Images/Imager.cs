using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConstructionSite.Helpers.Images
{
 public   class Imager
    {
        public static string GetImageSubFolder(string subFolder,string name)
        {
         return Path.Combine(subFolder, Guid.NewGuid().ToString() + Path.GetFileName(name));
        }
        
    }
}
