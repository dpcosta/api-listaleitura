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
                //incluir o livro na lista de leitura do usuário!!
                var userId = _userManager.GetUserId(User);
                _listaManager.IncluirLivro(userId, model.ToLivro(), model.Tipo);
                return RedirectToAction("Index", new { controller = "Home" });
            }
            HttpContext.Response.StatusCode = 400; //Bad Request
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
            _listaManager.MoverLivro(userId, livro, model.Origem, model.Destino);
            return Ok();
        }
    }
}