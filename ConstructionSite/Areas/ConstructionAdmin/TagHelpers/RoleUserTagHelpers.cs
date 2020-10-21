using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace ConstructionSite.Areas.ConstructionAdmin.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "identity-Role")]
    public class RoleUserTagHelpers : TagHelper
    {
        private UserManager<ApplicationUser> _UserManager;
        private RoleManager<IdentityRole> _RoleManager;

        public RoleUserTagHelpers(UserManager<ApplicationUser> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            this._UserManager = UserManager;
            this._RoleManager = RoleManager;
        }

        [HtmlAttributeName("identity-Role")]
        public string Role { get; set; }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            var roleResult = await _RoleManager.FindByIdAsync(Role);
            if (roleResult != null)
            {
                foreach (var user in _UserManager.Users)
                {
                    if (user != null && await _UserManager.IsInRoleAsync(user, roleResult.Name))
                    {
                        names.Add(user.Name);
                    }
                }
            }
            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(",", names));
        }
    }
}