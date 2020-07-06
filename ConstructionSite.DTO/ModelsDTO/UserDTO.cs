using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
