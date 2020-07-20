using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.Testimonial
{
   public class CustomerViewModel
    {
        public int id { get; set; }
        public string Content { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }
    }
}
