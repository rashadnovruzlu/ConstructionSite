using ConstructionSite.Helpers.Constants;

namespace ConstructionSite.Entity.Core
{
    public class NameContent
    {
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

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