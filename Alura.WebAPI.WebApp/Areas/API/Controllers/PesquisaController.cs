using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Alura.WebAPI.WebApp.Data;
using Alura.WebAPI.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.Areas.API.Controllers
{
    [Route("api/[controller]")]
    public class PesquisaController : Controller
    {
        private readonly LivrosManager _livrosManager;

        public PesquisaController(LivrosManager livrosManager)
        {
            _livrosManager = livrosManager;
        }

        private bool Pesquisa(Livro livro, Func<Livro, string> propriedade, string termo)
        {
            var resultado = propriedade(livro);
            return resultado.IndexOf(termo, StringComparison.OrdinalIgnoreCase)>=0;
        }

        private bool LivroAtendePesquisa(Livro livro, string termo)
        {
            bool porTitulo = Pesquisa(livro, l => l.Titulo, termo);
            bool porSubtitulo = Pesquisa(livro, l => l.Subtitulo, termo);
            bool porResumo = Pesquisa(livro, l => l.Resumo, termo);
            bool porAutor = Pesquisa(livro, l => l.Autor, termo);
            return porTitulo || porSubtitulo || porResumo || porAutor;
        }

        [HttpGet]
        public IActionResult Get(PesquisaViewModel model)
        {
            var livros = _livrosManager.All
            
                .Where(l => LivroAtendePesquisa(l, HttpUtility.HtmlDecode(model.Termo)))
                .ToList();
            if (livros.Count == 0)
            {
                return NoContent();
            }
            return Ok(livros);
        }
    }
}