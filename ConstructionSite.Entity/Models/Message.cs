using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class Message
    {
       
        public int Id { get; set; }
      
        public string Name { get; set; }
       
      
        public string Email { get; set; }
      
        public string Subject { get; set; }
      
        public string UserMessage { get; set; }
       
        public DateTime SendDate { get; set; }
      
        public bool IsAnswerd { get; set; }
    }
}
