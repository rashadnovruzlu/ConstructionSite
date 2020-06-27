using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConstructionSite.Helpers.Images
{
 public   class Imager
    {
        public static string GetImageSubFolder(string Folder,string name)
        {
         return Path.Combine(Folder, Guid.NewGuid().ToString() + Path.GetFileName(name));
        }
        
    }
}
