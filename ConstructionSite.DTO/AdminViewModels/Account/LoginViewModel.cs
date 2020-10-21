using System.ComponentModel.DataAnnotations;

namespace ConstructionSite.DTO.AdminViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}