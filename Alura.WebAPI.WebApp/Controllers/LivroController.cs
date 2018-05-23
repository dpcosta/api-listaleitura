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

namespace Alura.WebAPI.WebApp.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly LivrosManager _livrosManager;
        private readonly ListaManager _listaManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LivroController(LivrosManager livrosManager, ListaManager listaManager, UserManager<ApplicationUser> userManager)
        {
            _livrosManager = livrosManager;
            _userManager = userManager;
            _listaManager = listaManager;
        }

        [HttpGet]
        public IActionResult Novo(TiposDeListaLeitura tipo)
        {
            var model = new LivroNovoViewMovel
            {
                Tipo = tipo
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Novo(LivroNovoViewMovel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                //incluir o livro na lista de leitura!!
                var livro = new Livro
                {
                    Titulo = model.Titulo,
                    Subtitulo = model.Subtitulo,
                    Resumo = model.Resumo,
                    Autor = model.Autor,
                    Capa = model.Capa,
                };
                var lista = _listaManager.FindBy(userId, model.Tipo);
                if (lista != null)
                {
                    lista.Livros.Add(livro);
                    _listaManager.Alterar(lista);
                }
                else
                {
                    lista = new ListaLeitura
                    {
                        UsuarioId = userId,
                        Livros = new List<Livro> { livro },
                        Tipo = model.Tipo
                    };
                    _listaManager.Incluir(lista);
                }
                return RedirectToAction("Index", new { controller = "Home" });
            }
            HttpContext.Response.StatusCode = 400;
            return View(model);
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var livro = _livrosManager.Find(id);
            var model = new LivroDetalhesViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Subtitulo = livro.Subtitulo,
                Resumo = livro.Resumo,
                Autor = livro.Autor,
                Capa = livro.Capa
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Mover(LivroMoverViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var livro = _livrosManager.Find(model.LivroId);
            var listaOrigem = _listaManager.FindBy(userId, model.Origem);
            var listaDestino = _listaManager.FindBy(userId, model.Destino);
            listaOrigem.Livros.Remove(livro);
            listaDestino.Livros.Add(livro);
            _listaManager.Alterar(listaOrigem, listaDestino);
            return Ok();
        }
    }
}