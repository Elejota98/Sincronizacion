using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PagosFE
    {
        public long IdPago { get; set; }
        public string IdTransaccion { get; set; }
        public int? IdAutorizado { get; set; }
        public long IdEstacionamiento { get; set; }
        public string IdModulo { get; set; }
        public long IdFacturacion { get; set; }
        public long IdTipoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public float Subtotal { get; set; }
        public float Iva { get; set; }
        public float Total { get; set; }
        public int NumeroFactura { get; set; }
        public bool Sincronizacion { get; set; }
        public bool PagoMensual { get; set; }
        public bool Anulada { get; set; }
        public bool Estado { get; set; }
        public int Identificacion { get; set; }
        public int IdTipoVehiculo { get; set; }
    }
}
