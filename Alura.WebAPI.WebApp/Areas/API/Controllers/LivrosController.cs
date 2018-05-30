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

namespace Alura.WebAPI.WebApp.Areas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LivrosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ListaManager _listaManager;
        private readonly LivrosManager _livrosManager;

        public LivrosController(UserManager<ApplicationUser> userManager, LivrosManager livrosManager, ListaManager listaManager)
        {
            _userManager = userManager;
            _livrosManager = livrosManager;
            _listaManager = listaManager;
        }

        private IQueryable<Livro> LivrosDoUsuarioLogado => 
            _listaManager.All
            .Include(l => l.Livros)
            .Where(l => l.UsuarioId == _userManager.GetUserId(User))
            .SelectMany(l => l.Livros);

        [HttpGet]
        public IActionResult Get()
        {
            var lista = LivrosDoUsuarioLogado;
            if (lista.Count() == 0)
            {
                return NoContent();
            }
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var livro = LivrosDoUsuarioLogado
                .Where(l => l.Id == id)
                .FirstOrDefault();
            if (livro == null)
            {
                return NoContent();
            }
            return Ok(livro);
        }

        [HttpPost]
        public IActionResult Post(LivroNovoViewMovel model)
        {
            var livro = model.ToLivro();
            var lista = _listaManager.IncluirLivro(
                _userManager.GetUserId(User),
                livro,
                model.Tipo
            );
            var uri = Url.Action("Get", "Livros", new { id = livro.Id });
            return Created(uri, livro);
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
        public IActionResult Put(LivroDetalhesViewModel model)
        {
            var livro = _livrosManager.Find(model.Id);
            _livrosManager.Alterar(livro);
            return Ok();
        }
    }
}