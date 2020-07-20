using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConstructionSite.Helpers.Images
{
   public class Helper
    {
        public static void RemovePhotos(string photoPath)
        {


            if (File.Exists(photoPath))
            {
                File.Delete(photoPath);
            }
            
        }
    }
}
