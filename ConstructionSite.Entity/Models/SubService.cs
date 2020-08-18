using ConstructionSite.Entity.Core;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class SubService : NameContent
    {
        public int Id { get; set; }

        public virtual Service Service { get; set; }

        public virtual int ServiceId { get; set; }

        public virtual ICollection<SubServiceImage> SubServiceImages { get; set; }
    }
}