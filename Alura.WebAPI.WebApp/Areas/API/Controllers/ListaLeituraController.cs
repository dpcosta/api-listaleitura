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
            return Ok(lista);
        }

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
            return Ok(lista); //retornar um POCO faz com que o ASP.NET Core MVC embrulhe o objeto em um ObjectResult
            //mas e se quisermos retornar outros resultados (por exemplo, not found, internal server error?)
            //daí mudamos o retorno para IActionResult e usamos os métodos auxiliares Ok(), NotFound(), Json(), etc.
        }

    }
}