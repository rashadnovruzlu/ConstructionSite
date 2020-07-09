using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class CustomerFeedback
    {
        public int Id { get; set; }
     
        public string ContentAz { get; set; }
        
        public string ContentEn { get; set; }
        
        public string ContentRu { get; set; }
       
        public string FullName { get; set; }
       
        public string Position { get; set; }
    }
}
