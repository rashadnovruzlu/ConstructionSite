using ConstructionSite.Helpers.Constants;

namespace ConstructionSite.Entity.Core
{
    public class Name
    {
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

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
    }
}