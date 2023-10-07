using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Clientes
    {
        public int Identificacion { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreApellidos { get; set; }
        public string RazonSocial { get; set; }
        public int Empresa { get; set; }
        public int CodigoSucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int IdCiudad { get; set; }
        public int Vendedor { get; set; }
        public int CupoCredito { get; set; }
        public int ActividadEconomica { get; set; }
        public string ResponsabilidadFiscal { get; set; }
        public string Regimen { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
    }
}
