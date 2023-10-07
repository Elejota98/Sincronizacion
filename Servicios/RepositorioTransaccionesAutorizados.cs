using Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class RepositorioTransaccionesAutorizados
    {
        #region Definicion

        public static string sIdEstacionamientoNube
        {
            get
            {
                string sIdeModulo = "IdEstacionamiento" + ConfigurationManager.AppSettings["IdEstacionamiento"];
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
        #endregion

        #region Subida
        public DataTable ConsultarTransaccionesLocal(TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal)
        {
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            SqlDataReader rta;
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT top(1) *  FROM T_TransaccionesAutorizadosRed where IdEstacionamiento=" + transaccionesAutorizadosRedLocal.IdEstacionamiento + " AND Sincronizacion=0 ORDER BY FechaEntrada DESC");
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
        }

        public DataTable ValidarSiExisteTransaccionEnLaNube(TransaccionesAutorizadosRed transaccionesAutorizadosRed)
        {
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            SqlDataReader rta;
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("SELECT top(1) *  FROM T_TransaccionesAutorizadosRed where IdTransaccion=" + transaccionesAutorizadosRed.IdTransaccion + " ORDER BY FechaEntrada DESC");
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
        }

        public string ActualizaSalidaTransacciones(TransaccionesAutorizadosRed transaccionesAutorizadosRed)
        {
            string rta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("UPDATE T_TransaccionesAutorizadosRed SET FechaSalida = '" + transaccionesAutorizadosRed.FechaSalida?.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              ",  ModuloSalida = '" + transaccionesAutorizadosRed.ModuloSalida + "', " +
          " CarrilSalida = " + transaccionesAutorizadosRed.CarrilSalida + ",    PlacaSalida = '" + transaccionesAutorizadosRed.PlacaSalida + "',IdEstacionamiento8=" + transaccionesAutorizadosRed.IdEstacionamiento8 + "" +
          ", IdEstacionamiento9=" + transaccionesAutorizadosRed.IdEstacionamiento9 + ", IdEstacionamiento10=" + transaccionesAutorizadosRed.IdEstacionamiento10 + "" +
          ", IdEstacionamiento11=" + transaccionesAutorizadosRed.IdEstacionamiento11 + ", IdEstacionamiento12=" + transaccionesAutorizadosRed.IdEstacionamiento12 + " WHERE IdTransaccion=" + transaccionesAutorizadosRed.IdTransaccion + "");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";

            }
            catch (Exception ex)
            {

                rta = ex.Message.ToString();
            }

            return rta;
        }

        public string InsertarInformacionNube(TransaccionesAutorizadosRed transaccionesAutorizadosRed)
        {
            string rta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("INSERT INTO dbo.T_TransaccionesAutorizadosRed (IdTransaccion ,CarrilEntrada ,ModuloEntrada ,IdEstacionamiento ,IdTarjeta ,PlacaEntrada ,FechaEntrada ,FechaSalida," +
                    "ModuloSalida ,CarrilSalida ,PlacaSalida ,IdTipoVehiculo ,IdAutorizado ,IdEstacionamiento8 ,IdEstacionamiento9 ,IdEstacionamiento10 ,IdEstacionamiento11 ,IdEstacionamiento12) " +
                    "    VALUES (" + transaccionesAutorizadosRed.IdTransaccion + " ,'" + transaccionesAutorizadosRed.CarrilEntrada + "'" +
                    " ,'" + transaccionesAutorizadosRed.ModuloEntrada + "' ," + transaccionesAutorizadosRed.IdEstacionamiento + " ,'" + transaccionesAutorizadosRed.IdTarjeta + "','" + transaccionesAutorizadosRed.PlacaEntrada + "'" +
                    " ,'" + transaccionesAutorizadosRed.FechaEntrada.ToString("yyyy-MM-dd HH:mm:ss") + "' ,'" + transaccionesAutorizadosRed.FechaSalida?.ToString("yyyy-MM-dd HH:mm:ss") + "' ,'" + transaccionesAutorizadosRed.ModuloSalida + "' ,'" + transaccionesAutorizadosRed.CarrilSalida + "'" +
                    " ,'" + transaccionesAutorizadosRed.PlacaSalida + "' ," + transaccionesAutorizadosRed.IdTipoVehiculo + "," + transaccionesAutorizadosRed.IdAutorizado + " ," + transaccionesAutorizadosRed.IdEstacionamiento8 + "" +
                    " ," + transaccionesAutorizadosRed.IdEstacionamiento9 + " ," + transaccionesAutorizadosRed.IdEstacionamiento10 + " ," + transaccionesAutorizadosRed.IdEstacionamiento11 + " " +
                    "," + transaccionesAutorizadosRed.IdEstacionamiento12 + ")");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";
            }
            catch (Exception ex)
            {

                rta = ex.ToString();
            }

            return rta;

        }

        public string ActualizaEstadoSincronizacion(TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal)
        {
            string rta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("UPDATE T_TransaccionesAutorizadosRed SET Sincronizacion=1 WHERE IdTransaccion=" + transaccionesAutorizadosRedLocal.IdTransaccion + "");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";
            }
            catch (Exception ex)
            {

                rta = ex.ToString();
            }
            return rta;
        }

        #endregion

        #region Bajada
        public DataTable ConsultarTransaccionesNube(TransaccionesAutorizadosRed transaccionesAutorizadosRed)
        {
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            SqlDataReader rta;
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("SELECT top(1) *  FROM T_TransaccionesAutorizadosRed where " + sIdEstacionamientoNube + "= 0 ORDER BY FechaEntrada DESC");
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
        }

        public DataTable ValidarSiExisteTransaccionLocal(TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal)
        {
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            SqlDataReader rta;
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT top(1) *  FROM T_TransaccionesAutorizadosRed where IdTransaccion=" + transaccionesAutorizadosRedLocal.IdTransaccion + " ORDER BY FechaEntrada DESC");
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
        }

        public string ActualizaSalidaTransaccionesLocal(TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal)
        {
            string rta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("UPDATE T_TransaccionesAutorizadosRed SET FechaSalida = '" + transaccionesAutorizadosRedLocal.FechaSalida?.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              ",  ModuloSalida = '" + transaccionesAutorizadosRedLocal.ModuloSalida + "', " +
          " CarrilSalida = " + transaccionesAutorizadosRedLocal.CarrilSalida + " ,   PlacaSalida = '" + transaccionesAutorizadosRedLocal.PlacaSalida + "' WHERE IdTransaccion=" + transaccionesAutorizadosRedLocal.IdTransaccion + "");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";

            }
            catch (Exception ex)
            {

                rta = ex.Message.ToString();
            }

            return rta;
        }

        public string InsertarInformacionLocal(TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal)
        {
            string rta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("    INSERT INTO T_TransaccionesAutorizadosRed (IdTransaccion, CarrilEntrada,  ModuloEntrada, IdEstacionamiento,IdTarjeta," +
                    " PlacaEntrada,FechaEntrada, FechaSalida, ModuloSalida, CarrilSalida, PlacaSalida, IdTipoVehiculo, IdAutorizado,  Sincronizacion) " +
                    "    VALUES (" + transaccionesAutorizadosRedLocal.IdTransaccion + " ,'" + transaccionesAutorizadosRedLocal.CarrilEntrada + "'" +
                    " ,'" + transaccionesAutorizadosRedLocal.ModuloEntrada + "' ," + transaccionesAutorizadosRedLocal.IdEstacionamiento + " ,'" + transaccionesAutorizadosRedLocal.IdTarjeta + "','" + transaccionesAutorizadosRedLocal.PlacaEntrada + "'" +
                    " ,'" + transaccionesAutorizadosRedLocal.FechaEntrada.ToString("yyyy-MM-dd HH:mm:ss") + "' ,'" + transaccionesAutorizadosRedLocal.FechaSalida?.ToString("yyyy-MM-dd HH:mm:ss") + "' ,'" + transaccionesAutorizadosRedLocal.ModuloSalida + "' ,'" + transaccionesAutorizadosRedLocal.CarrilSalida + "'" +
                    " ,'" + transaccionesAutorizadosRedLocal.PlacaSalida + "' ," + transaccionesAutorizadosRedLocal.IdTipoVehiculo + ", " + transaccionesAutorizadosRedLocal.IdAutorizado + ", 1)");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";
            }
            catch (Exception ex)
            {

                rta = ex.ToString();
            }

            return rta;

        }

        public string ActualizaEstadoSincronizacionNube(TransaccionesAutorizadosRed transaccionesAutorizadosRed)
        {
            string rta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("UPDATE T_TransaccionesAutorizadosRed SET " + sIdEstacionamientoNube + " = 1 WHERE IdTransaccion=" + transaccionesAutorizadosRed.IdTransaccion + "");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rta = "OK";
            }
            catch (Exception ex)
            {

                rta = ex.ToString();
            }
            return rta;
        }


        #endregion
    }
}
