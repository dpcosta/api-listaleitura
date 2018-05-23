using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models.LivroViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Subtítulo")]
        public string Subtitulo { get; set; }

        public string Resumo { get; set; }
        public string Autor { get; set; }

        //[RegularExpression("")] //validar o caminho da imagem?
        public string Capa { get; set; }
    }
}
