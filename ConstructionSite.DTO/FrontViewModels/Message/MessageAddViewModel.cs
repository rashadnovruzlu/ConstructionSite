using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionSite.DTO.FrontViewModels.Message
{
    public class MessageAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string UserMessage { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsAnswerd { get; set; }
    }
}