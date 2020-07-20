using System;

namespace ConstructionSite.DTO.FrontViewModels.Message
{
    public class MessageAddViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Subject { get; set; }

        public string UserMessage { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsAnswerd { get; set; }
    }
}