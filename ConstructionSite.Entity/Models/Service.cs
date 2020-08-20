using ConstructionSite.Entity.Core;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class Service : NameTitle
    {
        public int Id { get; set; }
        public virtual Image Image { get; set; }
        public int ImageId { get; set; }

        public virtual ICollection<SubService> SubServices { get; set; }
    }
}