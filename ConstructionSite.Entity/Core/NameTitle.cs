using ConstructionSite.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Core
{
   public class NameTitle
    {
		public string NameAz { get; set; }

		public string NameEn { get; set; }

		public string NameRu { get; set; }

		public string TittleAz { get; set; }
		public string TittleEn { get; set; }
		public string TittleRu { get; set; }
        public virtual string FindName(string lang)
        {
            if (lang == LANGUAGECONSTANT.Az)
            {
                return NameAz;
            }
            else if (lang == LANGUAGECONSTANT.Ru)
            {
                return NameRu;
            }
            return NameEn;
        }
        public virtual string FindTitle(string lang)
        {
            if (lang == LANGUAGECONSTANT.Az)
            {
                return TittleAz;
            }
            else if (lang == LANGUAGECONSTANT.Ru)
            {
                return TittleRu;
            }
            return TittleEn;
        }

    }
}
