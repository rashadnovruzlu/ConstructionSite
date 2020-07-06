using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(maximumLength: 50)]
        [UIHint("username")]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email.")]
        [StringLength(maximumLength: 50)]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 50)]
        [UIHint("password")]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; } 
    }
}
