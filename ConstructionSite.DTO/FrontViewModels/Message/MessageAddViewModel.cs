using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionSite.DTO.FrontViewModels.Message
{
    public class MessageAddViewModel
    {
        //[Required(ErrorMessage = "please enter Name")]
        public string Name { get; set; }

        // [Required(ErrorMessage = "please enter email")]
        [UIHint("email")]
        public string Email { get; set; }

        // [Required(ErrorMessage = "please enter Subject")]
        public string Subject { get; set; }

        //  [Required(ErrorMessage = "please enter UserMessage")]
        public string UserMessage { get; set; }

        public DateTime SendDate { get; set; }
        public bool IsAnswerd { get; set; }
    }
}