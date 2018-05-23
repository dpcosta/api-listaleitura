using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models.AccountViewModels
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

    }
}
