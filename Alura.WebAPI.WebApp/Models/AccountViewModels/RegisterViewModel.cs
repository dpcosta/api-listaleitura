using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Login")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "Senha e confirmação são diferentes.")]
        public string ConfirmPassword { get; set; }
    }
}
