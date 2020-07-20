using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Extensions.Helpers
{
  public  static  class HelperExtension
    {
        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
           
            if (items.Count<0&&items==null)
            {
                
                return false;
            }
                return true;
        }
    }
}
