
using Microsoft.AspNetCore.Identity;

namespace ConstructionSite.Entity.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
