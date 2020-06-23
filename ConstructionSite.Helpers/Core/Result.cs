using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Helpers.Core
{
   public class Result<T>
    {
        public T Data { get; set; }
        public bool IsResult { get; set; }
    }
}
