using ConstructionSite.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Core
{
     public  class Content
    {
        public string ContentAz { get; set; }

        public string ContentEn { get; set; }

        public string ContentRu { get; set; }
        public virtual string FindContent(string lang)
        {
            if (lang == LANGUAGECONSTANT.Az)
            {
                return ContentAz;
            }
            else if (lang == LANGUAGECONSTANT.Ru)
            {
                return ContentRu;
            }
            return ContentEn;
        }
    }
}
