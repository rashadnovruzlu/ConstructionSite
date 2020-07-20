using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.Role
{
   public class RoleDetailsViewModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NoMembers { get; set; }
    }
}
