using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.WebAPI.WebApp.Data;
using Alura.WebAPI.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alura.WebAPI.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
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

        public ListaLeitura Index([FromRoute]TiposDeListaLeitura tipo)
        {
            var lista = ListaDoUsuarioPorTipo(tipo);
            //return View(); //não é mais HTML!!
            //return Json(lista); //explicitamente retornando Json
            return lista; //...ou posso retornar a lista direto!
        }

        //mas gostaria que fosse uma URL assim: /ListaLeitura/ParaLer
        [HttpGet("{tipo:regex(^ParaLer|Lendo|Lidos)}")]
        public IActionResult Get(TiposDeListaLeitura tipo)
        {
            var lista = ListaDoUsuarioPorTipo(tipo);
            //ForEach para remover a referência circular à própria lista, oq está gerando problemas na serialização Json
            lista.Livros.ToList().ForEach(l => l.Lista = null);
            return Ok(lista); //retornar um POCO faz com que o ASP.NET Core MVC embrulhe o objeto em um ObjectResult
            //mas e se quisermos retornar outros resultados (por exemplo, not found, internal server error?)
            //daí mudamos o retorno para IActionResult e usamos os métodos auxiliares Ok(), NotFound(), Json(), etc.
        }

    }
}