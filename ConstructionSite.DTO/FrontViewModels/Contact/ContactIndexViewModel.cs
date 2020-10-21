using System.ComponentModel.DataAnnotations;

namespace ConstructionSite.DTO.FrontViewModels.Contact
{
    public class ContactIndexViewModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }

        [UIHint("email")]
        public string Email { get; set; }

        [UIHint("phone")]
        public string PhoneNumber { get; set; }
    }
}