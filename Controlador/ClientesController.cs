using Modelo;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ClientesController
    {
        #region SincronizacionClientes

        public static string SincronizarClientes()
        {
            Clientes clientes = new Clientes();
            RepositorioClientes Datos = new RepositorioClientes();
            DataTable tabla;
            string rta = "";
            try
            {
                tabla = Datos.ConsultarClientesNube();
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow lstDatos in tabla.Rows)
                    {
                        clientes.Identificacion = Convert.ToInt32(lstDatos["Identificacion"]);
                        clientes.TipoPersona = lstDatos["TipoPersona"].ToString();
                        clientes.TipoDocumento = lstDatos["TipoDocumento"].ToString();
                        clientes.NombreApellidos = lstDatos["NombreApellidos"].ToString();
                        clientes.RazonSocial = lstDatos["RazonSocial"].ToString();
                        clientes.Empresa = Convert.ToInt32(lstDatos["Empresa"]);
                        clientes.CodigoSucursal = Convert.ToInt32(lstDatos["CodigoSucursal"]);
                        clientes.Direccion = lstDatos["Direccion"].ToString();
                        clientes.Telefono = lstDatos["Telefono"].ToString();
                        clientes.Email = lstDatos["Email"].ToString();
                        clientes.IdCiudad = Convert.ToInt32(lstDatos["IdCiudad"]);
                        clientes.Vendedor = Convert.ToInt32(lstDatos["Vendedor"]);
                        clientes.CupoCredito = Convert.ToInt32(lstDatos["CupoCredito"]);
                        clientes.ActividadEconomica = Convert.ToInt32(lstDatos["ActividadEconomica"]);
                        clientes.ResponsabilidadFiscal = lstDatos["ResponsabilidadFiscal"].ToString();
                        clientes.Regimen = lstDatos["Regimen"].ToString();
                        clientes.Fecha = Convert.ToDateTime(lstDatos["Fecha"]);
                        clientes.Estado = true;

                        tabla = Datos.ValidarClienteExiste(clientes);

                        if (tabla.Rows.Count <= 0)
                        {
                            rta = Datos.InsertarClienteLocal(clientes);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                return rta = ex.ToString();
            }

            return rta;
        }

        #endregion
    }
}
