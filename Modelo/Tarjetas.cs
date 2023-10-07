using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Tarjetas
    {
        public long IdEstacionamiento { get; set; }
        public string IdTarjeta { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string DocumentoUsuarioRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
