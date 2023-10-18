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
                string cadena = ("SELECT TOP(1) * FROM T_FacturasContingencia WHERE Sincronizacion=0 and IdEstacionamiento='"+facturasContingencia.IdEstacionamiento+"' ");
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

        public string InsertarFacturasContingencia(FacturasContingencia facturasContingencia)
        {
            string rta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNubeFacturacionElectronica();
                string cadena = "INSERT INTO T_FacturasContingencia (IdModulo, IdEstacionamiento, FechaPago, Subtotal, Iva, Total, Prefijo, IdTipoPago, NumeroFactura, Observaciones, " +
                    "IdTipoVehiculo,IdentificacionCliente, DocumentoUsuario, Sincronizacion)" +
                    " VALUES ('" + facturasContingencia.IdModulo + "', " + facturasContingencia.IdEstacionamiento + ", '" + facturasContingencia.FechaPago.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " '" + facturasContingencia.Subtotal + "', '" + facturasContingencia.Iva + "', '" + facturasContingencia.Total + "', '" + facturasContingencia.Prefijo + "'," +
                    " '" + facturasContingencia.IdTipoPago + "', '" + facturasContingencia.NumeroFactura + "',  '"+facturasContingencia.Observaciones+"', '" + facturasContingencia.IdTipoVehiculo + "', '"+facturasContingencia.IdentificacionCliente+"','" + facturasContingencia.DocumentoUsuario + "'," +
                    " 0)";
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rta;
        }

        public string InsertarFacturasContingenciaNube(FacturasContingencia facturasContingencia)
        {
            string rta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                if(facturasContingencia.IdTipoPago==1 && facturasContingencia.IdTipoVehiculo == 1)
                {
                    facturasContingencia.IdTipoVehiculo = 1;
                }
                if(facturasContingencia.IdTipoPago==1 && facturasContingencia.IdTipoVehiculo == 2)
                {
                    facturasContingencia.IdTipoVehiculo = 2;
                }
                if(facturasContingencia.IdTipoPago==2 && facturasContingencia.IdTipoVehiculo == 1)
                {
                    facturasContingencia.IdTipoVehiculo = 4;
                }
                if(facturasContingencia.IdTipoPago==2 && facturasContingencia.IdTipoVehiculo == 2)
                {
                    facturasContingencia.IdTipoVehiculo = 5;
                }
                if(facturasContingencia.IdTipoPago==3 && facturasContingencia.IdTipoVehiculo == 1)
                {
                    facturasContingencia.IdTipoVehiculo = 6;
                }
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = "INSERT INTO T_FacturasContingencia (IdModulo, IdEstacionamiento, FechaPago, Subtotal, Iva, Total, Prefijo, IdTipoPago, NumeroFactura, Observaciones, " +
                    "IdTipoVehiculo,IdentificacionCliente, DocumentoUsuario, Sincronizacion)" +
                    " VALUES ('" + facturasContingencia.IdModulo + "', " + facturasContingencia.IdEstacionamiento + ", '" + facturasContingencia.FechaPago.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " '" + facturasContingencia.Subtotal + "', '" + facturasContingencia.Iva + "', '" + facturasContingencia.Total + "', '" + facturasContingencia.Prefijo + "'," +
                    " '" + facturasContingencia.IdTipoPago + "', '" + facturasContingencia.NumeroFactura + "',  '" + facturasContingencia.Observaciones + "', '" + facturasContingencia.IdTipoVehiculo + "', '" + facturasContingencia.IdentificacionCliente + "','" + facturasContingencia.DocumentoUsuario + "'," +
                    " 0)";
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rta;
        }
        public string ActualizarEstadoSincronizacionContingencia(FacturasContingencia facturasContingencia)
        {
            string rta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("UPDATE T_FacturasContingencia SET Sincronizacion=1 WHERE IdPago=" + facturasContingencia.IdPago + "");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rta;
        }
    }
}
