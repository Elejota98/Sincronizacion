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
    public class PagosFEController
    {
        public static string SincronizaPagosFE()
        {
            PagosFE pagosFE = new PagosFE();
            RepositorioPagosFE Datos = new RepositorioPagosFE();
            DataTable tabla;
            string rta = "";
            int idTipoPagoNew = 0;
            try
            {
                do
                {
                    tabla = Datos.ConsultarPagosFE();
                    if (tabla.Rows.Count > 0)
                    {
                        foreach (DataRow lstDatos in tabla.Rows)
                        {
                            pagosFE.IdPago = Convert.ToInt64(lstDatos["IdPago"]);
                            pagosFE.IdTransaccion = lstDatos["IdTransaccion"].ToString();
                            pagosFE.IdAutorizado = lstDatos["IdAutorizado"] != DBNull.Value ? Convert.ToInt32(lstDatos["IdAutorizado"]) : (int?)null;
                            pagosFE.IdEstacionamiento = Convert.ToInt64(lstDatos["IdEstacionamiento"]);
                            if (pagosFE.IdAutorizado != null)
                            {
                                if (pagosFE.IdAutorizado > 0)
                                {
                                    tabla = Datos.ObtenerIdTipoPagoYTipoVehiculoAutorizacion(pagosFE);
                                    if (tabla.Rows.Count > 0)
                                    {
                                        foreach (DataRow lstDatosIds in tabla.Rows)
                                        {
                                            pagosFE.IdTipoPago = Convert.ToInt32(lstDatosIds["IdTipoPago"]);
                                            pagosFE.IdTipoVehiculo = Convert.ToInt32(lstDatosIds["IdTipoVehiculo"]);
                                        }
                                    }

                                }
                                else if (pagosFE.IdAutorizado == 0)
                                {
                                    tabla = Datos.ObtenerIdTipoVehiculoPorHoras(pagosFE);
                                    if (tabla.Rows.Count > 0)
                                    {
                                        foreach (DataRow lstDatosIds in tabla.Rows)
                                        {
                                            pagosFE.IdTipoVehiculo = Convert.ToInt32(lstDatosIds["IdTipoVehiculo"]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                tabla = Datos.ObtenerIdTipoVehiculoPorHoras(pagosFE);
                                if (tabla.Rows.Count > 0)
                                {
                                    foreach (DataRow lstDatosIds in tabla.Rows)
                                    {
                                        pagosFE.IdTipoVehiculo = Convert.ToInt32(lstDatosIds["IdTipoVehiculo"]);
                                    }
                                }
                            }
                            pagosFE.IdModulo = lstDatos["IdModulo"].ToString();
                            pagosFE.IdFacturacion = Convert.ToInt64(lstDatos["IdFacturacion"]);
                            pagosFE.IdTipoPago = Convert.ToInt64(lstDatos["IdTipoPago"]);
                            pagosFE.FechaPago = Convert.ToDateTime(lstDatos["FechaPago"]);
                            pagosFE.Subtotal = Convert.ToInt32(lstDatos["Subtotal"]);
                            pagosFE.Iva = Convert.ToInt32(lstDatos["Iva"]);
                            pagosFE.Total = Convert.ToInt32(lstDatos["Total"]);
                            pagosFE.NumeroFactura = Convert.ToInt32(lstDatos["NumeroFactura"]);
                            pagosFE.Sincronizacion = Convert.ToBoolean(lstDatos["Sincronizacion"]);
                            pagosFE.PagoMensual = Convert.ToBoolean(lstDatos["PagoMensual"]);
                            pagosFE.Anulada = Convert.ToBoolean(lstDatos["Anulada"]);
                            pagosFE.Estado = Convert.ToBoolean(lstDatos["Estado"]);
                            pagosFE.Identificacion = Convert.ToInt32(lstDatos["Identificacion"]);

                        }
                        rta = Datos.InsertarPagosFENube(pagosFE);

                        if (pagosFE.IdTipoPago == 6)
                        {
                            idTipoPagoNew = 5;
                        }
                        if (pagosFE.IdTipoPago == 3)
                        {
                            idTipoPagoNew = 6;
                        }

                        if (pagosFE.IdTipoPago == 1 && pagosFE.IdTipoVehiculo == 1)
                        {
                            idTipoPagoNew = 3;
                        }
                        else if (pagosFE.IdTipoPago == 1 && pagosFE.IdTipoVehiculo == 2)
                        {
                            idTipoPagoNew = 4;
                        }
                        if (pagosFE.IdTipoPago == 2 && pagosFE.IdTipoVehiculo == 1)
                        {
                            idTipoPagoNew = 1;
                        }
                        if (pagosFE.IdTipoPago == 2 && pagosFE.IdTipoVehiculo == 2)
                        {
                            idTipoPagoNew = 2;
                        }
                        pagosFE.IdTipoPago = idTipoPagoNew;

                        rta = Datos.InsertarPagosFE(pagosFE);
                        if (rta.Equals("OK"))
                        {
                            rta = Datos.ActualizaEstadoPagos(pagosFE);
                            if (rta.Equals("OK"))
                            {
                                rta = "OK";
                            }
                            else
                            {
                                rta = rta.ToString();
                            }
                        }
                    }
                }
                while (tabla.Rows.Count > 0);

            }
            catch (Exception ex )
            {

                throw ex;
            }
            return rta;
        }
    }
}
