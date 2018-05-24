using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.WebAPI.WebApp.Data;
using Alura.WebAPI.WebApp.Models;
using Alura.WebAPI.WebApp.Models.LivroViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alura.WebAPI.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ListaLeituraController : Controller
    {
        private readonly ListaManager _listaManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LivrosManager _livrosManager;

        public ListaLeituraController(ListaManager listaManager, UserManager<ApplicationUser> userManager, LivrosManager livroManager)
        {
            _listaManager = listaManager;
            _userManager = userManager;
            _livrosManager = livroManager;
        }

        private ListaLeitura ListaDoUsuarioPorTipo(TiposDeListaLeitura tipo)
        {
            var userId = _userManager.GetUserId(this.User);
            var lista = _listaManager.All
                .Include(l => l.Livros)
                .Where(l => (l.Tipo == tipo) && (l.UsuarioId == userId))
                .First();
            return lista;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _listaManager.All.Include(ll => ll.Livros).ToList();
            //ForEach para remover a referência circular à própria lista, oq está gerando problemas na serialização Json
            //lista.ForEach(ll => ll.Livros.ToList().ForEach(l => l.Lista = null));
            return Ok(lista);
        }

        //mas gostaria que fosse uma URL assim: /ListaLeitura/ParaLer
        [HttpGet("{tipo:alpha}")]
        public IActionResult Get(string tipo)
        {
            TiposDeListaLeitura tp = TiposDeListaLeitura.ParaLer;
            try
            {
                tp = Enum.Parse<TiposDeListaLeitura>(tipo, true);

            }
            catch (ArgumentException)
            {

                return NotFound();
            }
            var lista = ListaDoUsuarioPorTipo(tp);
            //ForEach para remover a referência circular à própria lista, oq está gerando problemas na serialização Json
            //lista.Livros.ToList().ForEach(l => l.Lista = null);
            return Ok(lista); //retornar um POCO faz com que o ASP.NET Core MVC embrulhe o objeto em um ObjectResult
            //mas e se quisermos retornar outros resultados (por exemplo, not found, internal server error?)
            //daí mudamos o retorno para IActionResult e usamos os métodos auxiliares Ok(), NotFound(), Json(), etc.
        }

        [HttpPost]
        public IActionResult Post(LivroNovoViewMovel model)
        {
            //retorna a lista após a inclusão do livro
            var lista = _listaManager.IncluirLivro(
                _userManager.GetUserId(User),
                model.ToLivro(),
                model.Tipo
            );
            //return NoContent();
            var uri = Url.Action("Get", "ListaLeitura", new { tipo = model.Tipo });
            //ForEach para remover a referência circular à própria lista, oq está gerando problemas na serialização Json
            //lista.Livros.ToList().ForEach(l => l.Lista = null);
            return Created(uri, lista); 
        }

        [HttpDelete("{livroId}")]
        public IActionResult Delete(int livroId)
        {
            var userId = _userManager.GetUserId(User);
            var livro = _livrosManager.Find(livroId);
            if (livro == null)
            {
                return NotFound();
            }
            var lista = _listaManager.Find(livro.ListaLeituraId);
            lista.Livros.Remove(livro);
            _listaManager.Alterar(lista);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Put([FromForm] LivroMoverViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var livro = _livrosManager.Find(model.LivroId);
            _listaManager.MoverLivro(userId, livro, model.Origem, model.Destino);
            return Ok();
        }

    }
}