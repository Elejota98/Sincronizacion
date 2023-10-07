using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PersonasAutorizadasNube
    {
        public string Documento { get; set; }
        public int IdAutorizacion { get; set; }
        public string NombreApellidos { get; set; }
        public string IdTarjeta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int DocumentoUsuarioCreacion { get; set; }
        public bool Estado { get; set; }
        public int EstadoNew { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Placa1 { get; set; }
        public string Placa2 { get; set; }
        public string Placa3 { get; set; }
        public string Placa4 { get; set; }
        public string Placa5 { get; set; }
        public long IdEstacionamiento8 { get; set; }
        public long IdEstacionamiento9 { get; set; }
        public long IdEstacionamiento10 { get; set; }
        public long IdEstacionamiento11 { get; set; }
        public long IdEstacionamiento12 { get; set; }
    }
}
