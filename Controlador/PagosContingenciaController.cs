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
    public class PagosContingenciaController
    {
        public static bool SincronizarPagosContingencia(FacturasContingencia facturasContingencia)
        {
            bool ok = false;
            DataTable tabla = new DataTable();
            int numeroFacturaAnterior = 0;
            RepositorioPagosContingencia Datos = new RepositorioPagosContingencia();
            try
            {
                tabla = Datos.ConsultarPagosContingencia(facturasContingencia);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow lstDatos in tabla.Rows)
                    {
                        facturasContingencia.IdModulo = lstDatos["IdModulo"].ToString();
                        facturasContingencia.IdEstacionamiento = Convert.ToInt32(lstDatos["IdEstacionamiento"]);
                        facturasContingencia.FechaPago = Convert.ToDateTime(lstDatos["FechaPago"]);
                        facturasContingencia.Subtotal = Convert.ToDecimal(lstDatos["Subtotal"]);
                        facturasContingencia.Iva = Convert.ToDecimal(lstDatos["Iva"]);
                        facturasContingencia.Total = Convert.ToDecimal(lstDatos["Total"]);
                        facturasContingencia.Prefijo = lstDatos["Prefijo"].ToString();
                        facturasContingencia.IdTipoPago = Convert.ToInt32(lstDatos["IdTipoPago"]);
                        facturasContingencia.NumeroFactura = Convert.ToInt32(lstDatos["NumeroFactura"]);
                        facturasContingencia.Observaciones = lstDatos["Observaciones"].ToString();
                        facturasContingencia.IdTipoVehiculo = Convert.ToInt32(lstDatos["IdTipoVehiculo"]);
                        facturasContingencia.IdentificacionCliente = lstDatos["IdentificacionCliente"].ToString();
                        facturasContingencia.DocumentoUsuario = Convert.ToInt32(lstDatos["DocumentoUsuario"]);
                        facturasContingencia.Sincronizacion = Convert.ToBoolean(lstDatos["Sincronizacion"]);
                    }
                }


            }
            catch (Exception ex )
            {

                return ok;
            }
            return ok;
        }
    }
}
