using Alura.WebAPI.WebApp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models
{
    public enum TiposDeListaLeitura
    {
        ParaLer,
        Lendo,
        Lidos
    }

    public class ListaLeitura
    {
        public int Id { get; set; }
        public TiposDeListaLeitura Tipo { get; set; }
        public ICollection<Livro> Livros { get; set; }
        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
