using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.DTO.FrontViewModels.Contact
{
  public  class ContactViewModel
    {
        public string Tittle { get; set; }

        public string Content { get; set; }
        public string Address { get; set; }
        [UIHint("email")]
        public string Email { get; set; }
        [UIHint("phone")]
        public string PhoneNumber { get; set; }
    }
}
