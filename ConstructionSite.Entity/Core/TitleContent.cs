using ConstructionSite.Helpers.Constants;

namespace ConstructionSite.Entity.Core
{
    public class TitleContent
    {
        public string TittleAz { get; set; }

        public string TittleEn { get; set; }

        public string TittleRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }

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