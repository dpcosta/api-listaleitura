using Alura.WebAPI.WebApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models
{
    public class Livro
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Resumo { get; set; }
        public string Autor { get; set; }
        public string Capa { get; set; }
        public ListaLeitura Lista { get; set; }
    }
}
