using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ConstructionSite.Helpers.Localization
{
   public class Localizer
    {
        public static string GetLocalizerPath<T>()
        {
            return new AssemblyName(typeof(T).GetTypeInfo().Assembly.FullName).Name;
        }
    }
}
