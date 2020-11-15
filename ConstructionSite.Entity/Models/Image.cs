using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }

        public string VideoPath { get; set; }

        public virtual Service Service { get; set; }

        public virtual ICollection<AboutImage> AboutImages { get; set; }

        public virtual ICollection<HomePage> HomePages { get; set; }

        public virtual ICollection<NewsImage> NewsImages { get; set; }

        public virtual ICollection<ProjectImage> ProjectImages { get; set; }

        public virtual ICollection<SubServiceImage> SubServiceImages { get; set; }

        public virtual ICollection<ServiceImage> ServiceImages { get; set; }

        public virtual ICollection<PortfolioImage> PortfolioImages { get; set; }

        public virtual ICollection<GaleryFile> GaleryFiles { get; set; }
        public virtual ICollection<SliderImage> SliderImages { get; set; }
    }
}