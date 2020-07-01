using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [UIHint("email")]
        [Display(Prompt ="EmailPlaceHolder")]
        public string Email { get; set; }
    }
}
