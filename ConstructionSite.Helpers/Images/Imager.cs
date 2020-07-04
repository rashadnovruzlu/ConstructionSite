using System;
using System.IO;

namespace ConstructionSite.Helpers.Images
{
    public   class Imager
    {
        public static string GetImageSubFolder(string Folder,string name)
        {
            string extension = Path.GetExtension(name);
         return Path.Combine(Folder, Guid.NewGuid().ToString() +extension);
       
        }
        
    }
}
