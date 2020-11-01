using ConstructionSite.Entity.Core;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class Service : NameTitleContent
    {
        public int Id { get; set; }

        public virtual ICollection<ServiceImage> ServiceImages { get; set; }
        public virtual ICollection<SubService> SubServices { get; set; }
    }
}