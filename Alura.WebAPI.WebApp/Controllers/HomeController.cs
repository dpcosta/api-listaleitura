using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alura.WebAPI.WebApp.Models;
using Alura.WebAPI.WebApp.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Alura.WebAPI.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly LivrosManager _livrosManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(LivrosManager livrosManager, UserManager<ApplicationUser> userManager)
        {
            _livrosManager = livrosManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                HomeViewModel model = new HomeViewModel
                {
                    ParaLer = _livrosManager.ParaLerDoUsuario(userId),
                    Lendo = _livrosManager.LendoDoUsuario(userId),
                    Lidos = _livrosManager.LidosDoUsuario(userId)
                };
                return View(model);
            }
            return View("NaoLogado");
        }

        public IActionResult Pesquisa(string termo)
        {
            return View("Pesquisa", termo);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
