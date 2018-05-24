using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.Controllers
{
    [Route("[controller]")]
    public class TestesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var array = new List<string> { "abacaxi", "abacate", "banana", "maçã" };
            return Ok(array);
        }
    }
}