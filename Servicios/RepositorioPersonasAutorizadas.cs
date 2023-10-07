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
    public class RepositorioPersonasAutorizadas
    {
        #region Definiciones
        public static string sIdEstacionamiento
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
        #endregion

        #region Subida
        public DataTable ConsultarInformacionPersonasAutorizadaslocal()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT top 1 * FROM T_RedUNAB WHERE Sincronizacion = 0 and NombreApellidos IS NOT NULL and FechaInicio IS NOT NULL");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
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

        public DataTable ConsultarInformacionPersonasAutorizadasNube(PersonasAutorizadasNube oPersonasAutorizadasNube)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("SELECT Documento,FechaInicio,FechaFin,IdTarjeta,IdAutorizacion,Placa1,Placa2,Placa3,Placa4,Placa5 FROM T_RedUNAB WHERE Documento = '" + oPersonasAutorizadasNube.Documento + "'");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
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

        public string ActualizaPersonasAutorizadasNube(PersonasAutorizadasNube oPersonasAutorizadasNube)
        {
            string rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                        string cadena = ("UPDATE T_RedUNAB SET IdAutorizacion = " + oPersonasAutorizadasNube.IdAutorizacion + ",NombreApellidos ='" + oPersonasAutorizadasNube.NombreApellidos + "',IdTarjeta ='" + oPersonasAutorizadasNube.IdTarjeta + "',FechaCreacion ='" + oPersonasAutorizadasNube.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss") +
                                "',DocumentoUsuarioCreacion =" + oPersonasAutorizadasNube.DocumentoUsuarioCreacion + ",Estado =" + oPersonasAutorizadasNube.EstadoNew + ",FechaInicio ='" + oPersonasAutorizadasNube.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss") + "',FechaFin ='" + oPersonasAutorizadasNube.FechaFin.ToString("yyyy-MM-dd HH:mm:ss") + "',Telefono ='" + oPersonasAutorizadasNube.Telefono + "',Email ='" + oPersonasAutorizadasNube.Email +
                                "',Placa1 ='" + oPersonasAutorizadasNube.Placa1 + "',Placa2 ='" + oPersonasAutorizadasNube.Placa2 + "', Placa3='" + oPersonasAutorizadasNube.Placa3 + "', Placa4='" + oPersonasAutorizadasNube.Placa4 + "', Placa5 ='" + oPersonasAutorizadasNube.Placa5 + "',IdEstacionamiento8='" + oPersonasAutorizadasNube.IdEstacionamiento8 + "', IdEstacionamiento9='" + oPersonasAutorizadasNube.IdEstacionamiento9 + "', IdEstacionamiento10='" + oPersonasAutorizadasNube.IdEstacionamiento10 +
                                "', IdEstacionamiento11='" + oPersonasAutorizadasNube.IdEstacionamiento11 + "', IdEstacionamiento12='" + oPersonasAutorizadasNube.IdEstacionamiento12 + "' WHERE Documento = '" + oPersonasAutorizadasNube.Documento + "'");
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

        public string ActualizaSincronizacionPersonasAutorizadasLocal(PersonasAutorizadasLocal oPersonasAutorizadasLocal)
        {
            string rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("UPDATE T_RedUNAB SET Sincronizacion = 1 " + " WHERE Documento = '" + oPersonasAutorizadasLocal.Documento + "'");
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

        public string InsertaInformacionNube(PersonasAutorizadasNube oPersonasAutorizadasNube)
        {
            string rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("INSERT INTO T_RedUNAB (Documento, IdAutorizacion, NombreApellidos, IdTarjeta,FechaCreacion,DocumentoUsuarioCreacion,Estado,FechaInicio,FechaFin,Telefono,Email,Placa1,Placa2,Placa3,Placa4,Placa5,IdEstacionamiento8,IdEstacionamiento9,IdEstacionamiento10,IdEstacionamiento11,IdEstacionamiento12) values('"
                            + oPersonasAutorizadasNube.Documento + "'," + oPersonasAutorizadasNube.IdAutorizacion + ", '" + oPersonasAutorizadasNube.NombreApellidos + "','" + oPersonasAutorizadasNube.IdTarjeta + "', '" + oPersonasAutorizadasNube.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            oPersonasAutorizadasNube.DocumentoUsuarioCreacion + "," + oPersonasAutorizadasNube.EstadoNew + ", '" + oPersonasAutorizadasNube.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + oPersonasAutorizadasNube.FechaFin.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                            oPersonasAutorizadasNube.Telefono + "', '" + oPersonasAutorizadasNube.Email + "','" + oPersonasAutorizadasNube.Placa1 + "','" + oPersonasAutorizadasNube.Placa2 + "','" + oPersonasAutorizadasNube.Placa3 + "','" + oPersonasAutorizadasNube.Placa4 + "', '" +
                            oPersonasAutorizadasNube.Placa5 + "'," + oPersonasAutorizadasNube.IdEstacionamiento8 + "," + oPersonasAutorizadasNube.IdEstacionamiento9 + "," + oPersonasAutorizadasNube.IdEstacionamiento10 + "," + oPersonasAutorizadasNube.IdEstacionamiento11 + "," + oPersonasAutorizadasNube.IdEstacionamiento12 + ")");
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

        #endregion

        #region Bajada

        public DataTable ConsultaInformacionPersonasAutorizadasNubeBajada()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("SELECT top 1 * FROM T_RedUNAB WHERE IdEstacionamiento" + sIdEstacionamiento + "= 0");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
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

        public DataTable ConsultarInformacionPersonasAutorizadasLocalBajada(PersonasAutorizadasLocal oPersonasAutorizadasLocal)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT Documento,IdAutorizacion,IdTarjeta,FechaInicio,FechaFin,Placa1,Placa2,Placa3,Placa4,Placa5 FROM T_PersonasAutorizadas WHERE Estado = 1 AND  Documento = '" + oPersonasAutorizadasLocal.Documento + "'");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
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
        public DataTable ValidarTarjetInventarioBajada(PersonasAutorizadasLocal oPersonasAutorizadasLocal)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT * FROM T_TARJETAS WHERE IdTarjeta='" + oPersonasAutorizadasLocal.IdTarjeta + "'");
                SqlCommand comando = new SqlCommand(cadena, sqlCon);
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
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

        public string ActualizaEstadoTarjeta(Tarjetas tarjetas)
        {
            string rta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("UPDATE T_Tarjetas SET Estado=1 WHERE IdTarjeta='" + tarjetas.IdTarjeta + "'");
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

        public string InsertaTarjetaLocalBajada(Tarjetas tarjetas)
        {
            string rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                tarjetas.FechaRegistro = DateTime.Now;
                tarjetas.DocumentoUsuarioRegistro = "1";
                int boolestado = Convert.ToBoolean(tarjetas.Estado) ? 1 : 0;
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("INSERT INTO T_Tarjetas (IdEstacionamiento,IdTarjeta,FechaRegistro,DocumentoUsuarioRegistro,Estado)" +
                                "VALUES(" + sIdEstacionamiento + ", '" + tarjetas.IdTarjeta + "', '" + tarjetas.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + tarjetas.DocumentoUsuarioRegistro + "', " + boolestado + ")");
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
        public string ActualizarInformacionLocalBajada(PersonasAutorizadasLocal oPersonasAutorizadasLocal)
        {
            string rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                int boolestado = Convert.ToBoolean(oPersonasAutorizadasLocal.Estado) ? 1 : 0;
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                //update a la tabla red unab en el campo idestacionamiento para poner en true en la nube update al registro que acaba de crear localmente
                string cadena = "UPDATE T_PersonasAutorizadas SET IdAutorizacion = " + oPersonasAutorizadasLocal.IdAutorizacion + ",NombreApellidos ='" + oPersonasAutorizadasLocal.NombreApellidos + "',IdTarjeta ='" + oPersonasAutorizadasLocal.IdTarjeta + "',FechaCreacion ='" + oPersonasAutorizadasLocal.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss") +
                "',DocumentoUsuarioCreacion =" + oPersonasAutorizadasLocal.DocumentoUsuarioCreacion + ",Estado =" + boolestado + ",FechaInicio ='" + oPersonasAutorizadasLocal.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss")
                + "',FechaFin ='" + oPersonasAutorizadasLocal.FechaFin.ToString("yyyy-MM-dd HH:mm:ss") + "',Telefono ='" + oPersonasAutorizadasLocal.Telefono + "',Email ='" + oPersonasAutorizadasLocal.Email +
                "',Placa1 ='" + oPersonasAutorizadasLocal.Placa1 + "',Placa2 ='" + oPersonasAutorizadasLocal.Placa2 + "', Placa3 ='" + oPersonasAutorizadasLocal.Placa3 + "', " +
                "Placa4 ='" + oPersonasAutorizadasLocal.Placa4 + "', Placa5 ='" + oPersonasAutorizadasLocal.Placa5 + "', IdEstacionamiento =" + sIdEstacionamiento + " WHERE Documento = '" + oPersonasAutorizadasLocal.Documento + "'";
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
        public string ActualizaSincronizacionRedUnab(PersonasAutorizadasLocal oPersonasAutorizadasLocal)
        {
            string Rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("UPDATE T_RedUNAB SET Sincronizacion = 1" + "  WHERE documento ='" + oPersonasAutorizadasLocal.Documento + "'");
                SqlCommand comando = new SqlCommand(cadena, sqlcon);
                sqlcon.Open();
                comando.ExecuteNonQuery();
                Rta = "OK";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) sqlcon.Close();
            }
            return Rta;

        }
        public string ActualizaSincronizacionNube(PersonasAutorizadasNube oPersonasAutorizadasNube)
        {
            string Rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = RepositorioConexion.getInstancia().CrearConexionNube();
                string cadena = ("UPDATE T_RedUNAB SET IdEstacionamiento" + sIdEstacionamiento + "= 1" + "  WHERE documento ='" + oPersonasAutorizadasNube.Documento + "'");
                SqlCommand comando = new SqlCommand(cadena, sqlcon);
                sqlcon.Open();
                comando.ExecuteNonQuery();
                Rta = "OK";

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open) sqlcon.Close();
            }
            return Rta;


        }
        public string InsertaInformacionLocal(PersonasAutorizadasNube oPersonasAutorizadasNube)
        {
            string rta = "";
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                DateTime fecha = Convert.ToDateTime(oPersonasAutorizadasNube.FechaCreacion);
                DateTime fecha1 = Convert.ToDateTime(oPersonasAutorizadasNube.FechaInicio);
                DateTime fecha2 = Convert.ToDateTime(oPersonasAutorizadasNube.FechaFin);

                int AÑO_create = fecha.Year;
                int MES_create = fecha.Month;
                int DIA_create = fecha.Day;
                int HORA_create = fecha.Hour;
                int MINUTO_create = fecha.Minute;
                int SEG_create = fecha.Second;

                int AÑO_inicio = fecha1.Year;
                int MES_inicio = fecha1.Month;
                int DIA_inicio = fecha1.Day;
                int HORA_inicio = fecha1.Hour;
                int MINUTO_inicio = fecha1.Minute;
                int SEG_inicio = fecha1.Second;

                int AÑO_fin = fecha2.Year;
                int MES_fin = fecha2.Month;
                int DIA_fin = fecha2.Day;
                int HORA_fin = fecha2.Hour;
                int MINUTO_fin = fecha2.Minute;
                int SEG_fin = fecha2.Second;

                string fecha_create = AÑO_create.ToString() + "-" + MES_create.ToString() + "-" + DIA_create.ToString() + " " + HORA_create.ToString() + ":" + MINUTO_create.ToString() + ":" + SEG_create.ToString();
                string fecha_inicio = AÑO_inicio.ToString() + "-" + MES_inicio.ToString() + "-" + DIA_inicio.ToString() + " " + HORA_inicio.ToString() + ":" + MINUTO_inicio.ToString() + ":" + SEG_inicio.ToString();
                string fecha_fin = AÑO_fin.ToString() + "-" + MES_fin.ToString() + "-" + DIA_fin.ToString() + " " + HORA_fin.ToString() + ":" + MINUTO_fin.ToString() + ":" + SEG_fin.ToString();

                int boolestado = Convert.ToBoolean(oPersonasAutorizadasNube.Estado) ? 1 : 0;
                int boolsincr = 1;
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("INSERT INTO t_personasAutorizadas (Documento,IdAutorizacion,IdEstacionamiento,NombreApellidos,IdTarjeta,FechaCreacion,DocumentoUsuarioCreacion,Estado,Sincronizacion,FechaInicio,FechaFin,Telefono,Email,Placa1,Placa2,Placa3,Placa4,Placa5,ValorBolsa) values('"
                            + oPersonasAutorizadasNube.Documento + "'," + oPersonasAutorizadasNube.IdAutorizacion + ", '" + sIdEstacionamiento + "', '" + oPersonasAutorizadasNube.NombreApellidos + "','" + oPersonasAutorizadasNube.IdTarjeta + "', '" + fecha_create + "'," + oPersonasAutorizadasNube.DocumentoUsuarioCreacion + "," + boolestado + "," + boolsincr + ", '" + fecha_inicio + "', '" + fecha_fin + "','" + oPersonasAutorizadasNube.Telefono +
                            "', '" + oPersonasAutorizadasNube.Email + "','" + oPersonasAutorizadasNube.Placa1 + "','" + oPersonasAutorizadasNube.Placa2 + "', '" + oPersonasAutorizadasNube.Placa3 + "','" + oPersonasAutorizadasNube.Placa4 + "','" + oPersonasAutorizadasNube.Placa5 + "'," + 0 + ")");
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

        #endregion
    }
}
