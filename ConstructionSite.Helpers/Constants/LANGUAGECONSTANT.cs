using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConstructionSite.Helpers.Constants
{
    public sealed class LANGUAGECONSTANT
    {
        public const string Az = "az-Latn-AZ";
        public const string En = "en-US";
        public const string Ru = "ru-RU";

        public static List<string> GetLanguages()
        {
            List<string> Language = new List<string>
            {
                Az,
                En,
                Ru
            };
            return Language;
        }
        public static Dictionary<string, string> GetCountryProviders()
        {
            Dictionary<string, string> map =
                new Dictionary<string, string>
                {
                    {"az",Az},
                    {"us",En},
                    {"ru",Ru},
                  
                };

            return map;
        }
        public static CultureInfo[] GetSupportedCulture()
        {
            return GetLanguages().Select(x => new CultureInfo(x)).ToArray();
        }
    }
}