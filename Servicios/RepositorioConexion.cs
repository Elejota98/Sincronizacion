using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class RepositorioConexion
    {
        private static RepositorioConexion con = null;
        public SqlConnection CrearConexionNube()
        {
            SqlConnection cadena = new SqlConnection(ConfigurationManager.AppSettings["ConexionNube"]);
            return cadena;
        }
        public SqlConnection CrearConexionLocal()
        {
            SqlConnection cadena = new SqlConnection(ConfigurationManager.AppSettings["ConexionLocal"]);
            return cadena;
        }
        public SqlConnection CrearConexionNubeFacturacionElectronica()
        {
            SqlConnection cadena = new SqlConnection(ConfigurationManager.AppSettings["ConexionNubeFacturacionElectronica"]);
            return cadena;
        }

        public static RepositorioConexion getInstancia()
        {
            if (con == null)
            {
                con = new RepositorioConexion();
            }
            return con;

        }

    }
}
