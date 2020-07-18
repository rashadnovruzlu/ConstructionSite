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
       
       // [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
      
        public string Subject { get; set; }
      
        public string UserMessage { get; set; }
        //[Required]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}{1:HH/mm}")]
        public DateTime SendDate { get; set; }
        //[Required]
        //[RegularExpression("False")]
        public bool IsAnswerd { get; set; }
    }
}
