﻿using ConstructionSite.Helpers.Constants;

namespace ConstructionSite.Entity.Core
{
    public class Title
    {
        public string TitleAz { get; set; }

        public string TitleEn { get; set; }

        public string TitleRu { get; set; }

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
    }
}