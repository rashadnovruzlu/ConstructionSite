using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConstructionSite.Helpers.Constants
{
  public   class LANGUAGECONSTANT
    {
        public const string Az = "az-Latn";
        public const string En = "en-US";
        public const string Ru = "ru-RU";
        public static List<string> GetLanguage()
        {
            List<string> Language = new List<string>
            {
                Az,
                En,
                Ru
            };
            return Language;
        }
        public static CultureInfo[] GetCultureInfo()
        {
            return GetLanguage().Select(x=>new CultureInfo(x)).ToArray();
        }
    }
}
