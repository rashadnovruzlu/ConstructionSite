using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Models.ViewModels.Account
{
    public class UserEditModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email.")]
        [StringLength(maximumLength: 50)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 50)]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
