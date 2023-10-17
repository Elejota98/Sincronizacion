using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class RepositorioClientes
    {
        public DataTable ConsultarClientesNube()
        {
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            SqlDataReader rta;
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionNubeFacturacionElectronica();
                string cadena = ("SELECT TOP (1) * FROM T_Clientes WHERE Estado=1 ORDER BY FECHA DESC  ");
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

        public string InsertarClienteLocal(Clientes clientes)
        {
            SqlConnection sqlCon = new SqlConnection();
            string rta = "";
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("INSERT INTO T_Clientes(Identificacion, TipoPersona,TipoDocumento,NombreApellidos, RazonSocial, Empresa, CodigoSucursal,  Direccion, Telefono, Email, IdCiudad, " +
                    "Vendedor, CupoCredito,ActividadEconomica,ResponsabilidadFiscal,Regimen,Rut,Fecha,Estado)" +
                    "VALUES(" + clientes.Identificacion + ",'" + clientes.TipoPersona + "', '" + clientes.TipoDocumento + "', '" + clientes.NombreApellidos + "', '" + clientes.RazonSocial + "'," +
                    "'" + clientes.Empresa + "', '" + clientes.CodigoSucursal + "', '" + clientes.Direccion + "', '" + clientes.Telefono + "', '" + clientes.Email + "', '" + clientes.IdCiudad + "', '" + clientes.Vendedor + "'," +
                    "'" + clientes.CupoCredito + "', '" + clientes.ActividadEconomica + "','" + clientes.ResponsabilidadFiscal + "' ,'" + clientes.Regimen + "', NULL, '" + clientes.Fecha.ToString("yyyy-dd-MM HH:mm:ss") + "', 1)");
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

        public DataTable ValidarClienteExiste(Clientes clientes)
        {
            SqlConnection sqlCon = new SqlConnection();
            DataTable tabla = new DataTable();
            SqlDataReader rta;
            try
            {
                sqlCon = RepositorioConexion.getInstancia().CrearConexionLocal();
                string cadena = ("SELECT * FROM T_Clientes WHERE Identificacion = '" + clientes.Identificacion + "' AND ESTADO=1 ");
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
    }
}
