using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Servicios
{

    public class RepositorioPagosContingencia
    {
        public static string sIdEstacionamientoNube
        {
            get
            {
                string sIdeModulo = ConfigurationManager.AppSettings["IdEstacionamiento"];
                if (!string.IsNullOrEmpty(sIdeModulo))
                {
                    return sIdeModulo;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public DataTable ConsultarPagosContingencia(FacturasContingencia facturasContingencia) 
        {
            SqlDataReader rta;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT TOP(1) * FROM T_FacturasContingencia WHERE Sincronizacion=0 amd IdEstacionamiento='"+facturasContingencia+"' ");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                rta = comando.ExecuteReader();
                tabla.Load(rta);
                return tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }
    }
}
