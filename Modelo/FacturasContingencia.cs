using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class FacturasContingencia
    {
        public string IdModulo { get; set; }
        public int IdEstacionamiento { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string Prefijo { get; set; }
        public int IdTipoPago { get; set; }
        public int NumeroFactura { get; set; }
        public string Observaciones { get; set; }
        public int IdTipoVehiculo { get; set; }
        public string IdentificacionCliente { get; set; }
        public int DocumentoUsuario { get; set; }
        public bool Sincronizacion { get; set; }
    }
}
