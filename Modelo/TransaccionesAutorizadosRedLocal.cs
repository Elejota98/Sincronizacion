using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class TransaccionesAutorizadosRedLocal
    {
        public long IdTransaccion { get; set; }
        public int CarrilEntrada { get; set; }
        public string ModuloEntrada { get; set; }
        public long IdEstacionamiento { get; set; }
        public string IdTarjeta { get; set; }
        public string PlacaEntrada { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string ModuloSalida { get; set; }
        public int? CarrilSalida { get; set; }
        public string PlacaSalida { get; set; }
        public int IdTipoVehiculo { get; set; }
        public long IdAutorizado { get; set; }
        public bool Sincronizacion { get; set; }
    }
}
