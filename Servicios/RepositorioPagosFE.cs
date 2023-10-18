using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Modelo;

namespace Servicios
{
    public class RepositorioPagosFE
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


        public DataTable ConsultarPagosFE()
        {
            SqlDataReader rta;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT TOP(1) * FROM T_PagosFE WHERE Sincronizacion=0 AND IdEstacionamiento='" + sIdEstacionamientoNube + "' ORDER BY FechaPago DESC  ");
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

        public DataTable ObtenerIdTipoPagoYTipoVehiculoAutorizacion(PagosFE pagosFE)
        {
            SqlDataReader rta;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT IdTipoVehiculo,IdTipoPago FROM T_TARIFAS WHERE IdAutorizacion="+pagosFE.IdAutorizado+" and IdEstacionamiento="+pagosFE.IdEstacionamiento+" ");
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

        public DataTable ObtenerIdTipoVehiculoPorHoras(PagosFE pagosFE)
        {
            SqlDataReader rta;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT IdTipoVehiculo FROM T_Transacciones WHERE IdTransaccion='"+pagosFE.IdTransaccion+"' and IdEstacionamiento=" + pagosFE.IdEstacionamiento + " ");
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

        public string InsertarPagosFE(PagosFE pagosFE)
        {
            string rta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                pagosFE.Sincronizacion = true;
                pagosFE.Estado = false;
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNubeFacturacionElectronica();
                string cadena = ("INSERT INTO T_Pagos (NumeroDocumento, NumeroFactura, Prefijo, Total, IdEstacionamiento, IdTipoPago, FechaPago, Imagen, Estado, FechaSolicitud)" +
                    "VALUES ('"+pagosFE.Identificacion+"', '"+pagosFE.NumeroFactura+"', '"+pagosFE.IdModulo+"', '"+pagosFE.Total+"',"+pagosFE.IdEstacionamiento+" , '"+pagosFE.IdTipoPago+"', '"+pagosFE.FechaPago.ToString("yyyy-MM-dd HH:mm:ss") +"', NULL," +
                    "'"+pagosFE.Estado+"', GETDATE() )");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";

            }
            catch (Exception ex )
            {

                throw ex;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rta;
        }

        public string InsertarPagosFENube(PagosFE pagosFE)
        {
            string rta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                pagosFE.Sincronizacion = true;
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("INSERT INTO T_PagosFE (IdTransaccion,IdAutorizado,IdEstacionamiento,IdModulo,IdFacturacion,IdTipoPago,FechaPago,Subtotal,Iva,Total,NumeroFactura,Sincronizacion,PagoMensual,Anulada) " +
                    "VALUES(" + pagosFE.IdTransaccion + ", '" + pagosFE.IdAutorizado + "', '" + pagosFE.IdEstacionamiento + "', '" + pagosFE.IdModulo + "', '" + pagosFE.IdFacturacion + "'," +
                    "'" + pagosFE.IdTipoPago + "', '" + pagosFE.FechaPago.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + pagosFE.Subtotal + "', '" + pagosFE.Iva + "', '" + pagosFE.Total + "', '" + pagosFE.NumeroFactura + "',1,0,0)");
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

        public string ActualizaEstadoPagos(PagosFE pagosFE)
        {
            string rta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("UPDATE T_PagosFE SET Sincronizacion=1 WHERE IdPago='" + pagosFE.IdPago + "'");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";
            }
            catch (Exception ex )
            {

                throw ex ;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rta;
        }

    }
}
