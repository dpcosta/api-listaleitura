using Alura.WebAPI.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Data
{
    public class ListaManager : BaseManager<ListaLeitura, int>
    {
        public ListaManager(ApplicationDbContext ctx) : base(ctx)
        {

        }

        public ListaLeitura FindBy(string userId, TiposDeListaLeitura tipo)
        {
            return Contexto.ListasDeLeitura
                .Include(l => l.Livros)
                .FirstOrDefault(l => (l.UsuarioId == userId) && (l.Tipo == tipo));
        }
    }
}
