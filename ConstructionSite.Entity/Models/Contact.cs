using ConstructionSite.Entity.Core;

namespace ConstructionSite.Entity.Models
{
    public class Contact : TitleContent
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}