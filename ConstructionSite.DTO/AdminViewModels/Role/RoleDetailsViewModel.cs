using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ConstructionSite.DTO.AdminViewModels.Role
{
    public class RoleDetailsViewModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NoMembers { get; set; }
    }
}