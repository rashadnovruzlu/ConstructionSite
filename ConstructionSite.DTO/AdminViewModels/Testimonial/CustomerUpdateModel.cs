using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.Testimonial
{
   public class CustomerUpdateModel
    {
        public int Id { get; set; }

        public string ContentAz { get; set; }

        public string ContentEn { get; set; }

        public string ContentRu { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }
    }
}
