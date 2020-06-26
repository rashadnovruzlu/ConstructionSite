using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Helpers.Interfaces
{
  public  interface ISharedLocalizationHandle
    {
      string  GetLocalizationByKey(string key);
    }
}
