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
            var lista = Contexto.ListasDeLeitura
                .Include(l => l.Livros)
                .FirstOrDefault(l => (l.UsuarioId == userId) && (l.Tipo == tipo));
            return lista;
        }

        public ListaLeitura IncluirLivro(string userId, Livro livro, TiposDeListaLeitura tipo)
        {
            var lista = FindBy(userId, tipo);
            if (lista != null)
            {
                lista.Livros.Add(livro);
                this.Alterar(lista);
            }
            else
            {
                lista = new ListaLeitura
                {
                    UsuarioId = userId,
                    Tipo = tipo,
                    Livros = new List<Livro> { livro }
                };
                this.Incluir(lista);
            }
            return lista;
        }

        public void MoverLivro(string userId, Livro livro, TiposDeListaLeitura origem, TiposDeListaLeitura destino)
        {
            var listaOrigem = this.FindBy(userId, origem);
            var listaDestino = this.FindBy(userId, destino);
            listaOrigem.Livros.Remove(livro);
            listaDestino.Livros.Add(livro);
            this.Alterar(listaOrigem, listaDestino);
        }
    }
}
