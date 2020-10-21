using ConstructionSite.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Core
{
    public class NameTitleContent
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }

        public string TitleAz { get; set; }
        public string TitleEn { get; set; }
        public string TitleRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }

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
                return TitleAz;
            }
            else if (lang == LANGUAGECONSTANT.Ru)
            {
                return TitleRu;
            }
            return TitleEn;
        }

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
