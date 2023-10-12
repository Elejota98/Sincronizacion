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
            int idTipoPagoNew = 0;
            string rta = string.Empty;
            RepositorioPagosContingencia Datos = new RepositorioPagosContingencia();
            try
            {
                do
                {
                    tabla = Datos.ConsultarPagosContingencia(facturasContingencia);
                    if (tabla.Rows.Count > 0)
                    {
                        foreach (DataRow lstDatos in tabla.Rows)
                        {
                            facturasContingencia.IdPago = Convert.ToInt32(lstDatos["IdPago"]);
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
                        rta = Datos.InsertarFacturasContingenciaNube(facturasContingencia);

                        if (facturasContingencia.IdTipoPago == 6)
                        {
                            idTipoPagoNew = 5;
                        }
                        if (facturasContingencia.IdTipoPago == 3)
                        {
                            idTipoPagoNew = 6;
                        }

                        if (facturasContingencia.IdTipoPago == 1 && facturasContingencia.IdTipoVehiculo == 1)
                        {
                            idTipoPagoNew = 3;
                        }
                        else if (facturasContingencia.IdTipoPago == 1 && facturasContingencia.IdTipoVehiculo == 2)
                        {
                            idTipoPagoNew = 4;
                        }
                        if (facturasContingencia.IdTipoPago == 2 && facturasContingencia.IdTipoVehiculo == 1)
                        {
                            idTipoPagoNew = 1;
                        }
                        if (facturasContingencia.IdTipoPago == 2 && facturasContingencia.IdTipoVehiculo == 2)
                        {
                            idTipoPagoNew = 2;
                        }
                        facturasContingencia.IdTipoPago = idTipoPagoNew;

                        rta = Datos.InsertarFacturasContingencia(facturasContingencia);
                        if (rta.Equals("OK"))
                        {
                            rta = Datos.ActualizarEstadoSincronizacionContingencia(facturasContingencia);
                            if (rta.Equals("OK"))
                            {
                                ok = true;
                            }
                            else
                            {
                                return ok;
                            }
                        }
                        else
                        {
                            return ok;
                        }


                    }
                }
                while (tabla.Rows.Count > 0);


            }
            catch (Exception ex )
            {

                return ok;
            }
            return ok;
        }
    }
}
