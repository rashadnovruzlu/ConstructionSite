using System;

namespace ConstructionSite.DTO.AdminViewModels.message
{
    public class MesageViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public DateTime SendDate { get; set; }
        public string Content { get; set; }
        public bool IsAnswerd { get; set; }
    }
}