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

namespace Alura.WebAPI.WebApp.Areas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MovimentacaoController : Controller
    {
        private readonly ListaManager _listaManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LivrosManager _livrosManager;

        public MovimentacaoController(ListaManager listaManager, UserManager<ApplicationUser> userManager, LivrosManager livroManager)
        {
            _listaManager = listaManager;
            _userManager = userManager;
            _livrosManager = livroManager;
        }

        [HttpPost]
        public IActionResult Post([FromForm] LivroMoverViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var livro = _livrosManager.Find(model.LivroId);
            _listaManager.MoverLivro(userId, livro, model.Origem, model.Destino);
            return Ok();
        }
    }
}