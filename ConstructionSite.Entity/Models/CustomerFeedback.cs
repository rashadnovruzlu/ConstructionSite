using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class CustomerFeedback:Content
    {
        public int Id { get; set; }
     
       
       
        public string FullName { get; set; }
       
        public string Position { get; set; }
    }
}
