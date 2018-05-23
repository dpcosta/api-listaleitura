using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Models.LivroViewModels
{
    public class LivroMoverViewModel
    {
        public int LivroId { get; set; }
        public TiposDeListaLeitura Origem { get; set; }
        public TiposDeListaLeitura Destino { get; set; }
    }
}
