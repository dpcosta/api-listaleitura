using Alura.WebAPI.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Data
{
    public class LivrosManager : BaseManager<Livro, int>
    {
        public LivrosManager(ApplicationDbContext ctx) : base(ctx)
        {

        }

        private ICollection<Livro> ListaDoUsuario(string userId, TiposDeListaLeitura tipo)
        {
            var lista = Contexto.ListasDeLeitura
                .Include(ll => ll.Livros)
                .Where(ll =>
                    (ll.UsuarioId == userId) &&
                    (ll.Tipo == tipo))
                .FirstOrDefault();
            return (lista != null) ? lista.Livros : new List<Livro>();

        }

        public ICollection<Livro> ParaLerDoUsuario(string userId)
        {
            return ListaDoUsuario(userId, TiposDeListaLeitura.ParaLer);
        }

        public ICollection<Livro> LendoDoUsuario(string userId)
        {
            return ListaDoUsuario(userId, TiposDeListaLeitura.Lendo);
        }

        public ICollection<Livro> LidosDoUsuario(string userId)
        {
            return ListaDoUsuario(userId, TiposDeListaLeitura.Lidos);
        }
    }
}
