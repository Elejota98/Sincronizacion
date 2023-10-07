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
    public class TransaccionesAutorizadosController
    {
        #region Subida
        public static string SincronizarTransaccionesSubida(TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal)
        {
            DataTable tabla;
            string rta = "";
            RepositorioTransaccionesAutorizados Datos = new RepositorioTransaccionesAutorizados();
            TransaccionesAutorizadosRed transaccionesAutorizadosRed = new TransaccionesAutorizadosRed();
            try
            {
                tabla = Datos.ConsultarTransaccionesLocal(transaccionesAutorizadosRedLocal);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow lstDatos in tabla.Rows)
                    {
                        transaccionesAutorizadosRedLocal.IdTransaccion = Convert.ToInt64(lstDatos["IdTransaccion"]);
                        transaccionesAutorizadosRedLocal.CarrilEntrada = Convert.ToInt32(lstDatos["CarrilEntrada"]);
                        transaccionesAutorizadosRedLocal.ModuloEntrada = lstDatos["ModuloEntrada"].ToString();
                        transaccionesAutorizadosRedLocal.IdEstacionamiento = Convert.ToInt64(lstDatos["IdEstacionamiento"]);
                        transaccionesAutorizadosRedLocal.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                        transaccionesAutorizadosRedLocal.PlacaEntrada = lstDatos["PlacaEntrada"].ToString();
                        transaccionesAutorizadosRedLocal.FechaEntrada = Convert.ToDateTime(lstDatos["FechaEntrada"]);
                        transaccionesAutorizadosRedLocal.FechaSalida = lstDatos["FechaSalida"] != DBNull.Value ? Convert.ToDateTime(lstDatos["FechaSalida"]) : (DateTime?)null;
                        transaccionesAutorizadosRedLocal.ModuloSalida = lstDatos["ModuloSalida"].ToString();
                        transaccionesAutorizadosRedLocal.CarrilSalida = lstDatos["CarrilSalida"] != DBNull.Value ? Convert.ToInt32(lstDatos["CarrilSalida"]) : (int?)null;
                        transaccionesAutorizadosRedLocal.PlacaSalida = lstDatos["PlacaSalida"].ToString();
                        transaccionesAutorizadosRedLocal.IdTipoVehiculo = Convert.ToInt32(lstDatos["IdTipoVehiculo"]);
                        transaccionesAutorizadosRedLocal.IdAutorizado = Convert.ToInt32(lstDatos["IdAutorizado"]);
                    }



                    //Mapeo los datos de la clase a la otra 

                    Mapeo(transaccionesAutorizadosRedLocal, transaccionesAutorizadosRed);

                    transaccionesAutorizadosRed.IdEstacionamiento8 = 0;
                    transaccionesAutorizadosRed.IdEstacionamiento9 = 0;
                    transaccionesAutorizadosRed.IdEstacionamiento10 = 0;
                    transaccionesAutorizadosRed.IdEstacionamiento11 = 0;
                    transaccionesAutorizadosRed.IdEstacionamiento12 = 1;

                    tabla = Datos.ValidarSiExisteTransaccionEnLaNube(transaccionesAutorizadosRed);
                    if (tabla.Rows.Count > 0)
                    {

                        foreach (DataRow lstDatos in tabla.Rows)
                        {

                            transaccionesAutorizadosRed.FechaSalida = lstDatos["FechaSalida"] != DBNull.Value ? Convert.ToDateTime(lstDatos["FechaSalida"]) : (DateTime?)null;
                            transaccionesAutorizadosRed.ModuloSalida = lstDatos["ModuloSalida"].ToString();
                            transaccionesAutorizadosRed.CarrilSalida = lstDatos["CarrilSalida"] != DBNull.Value ? Convert.ToInt32(lstDatos["CarrilSalida"]) : (int?)null;
                            transaccionesAutorizadosRed.PlacaSalida = lstDatos["PlacaSalida"].ToString();
                        }

                        if (transaccionesAutorizadosRed.ModuloSalida == transaccionesAutorizadosRedLocal.ModuloSalida)
                        {
                            rta = Datos.ActualizaEstadoSincronizacion(transaccionesAutorizadosRedLocal);
                            if (rta.Equals("OK"))
                            {
                                rta = "OK";
                            }
                            else
                            {
                                return rta;
                            }
                        }
                        else
                        {
                            Mapeo(transaccionesAutorizadosRedLocal, transaccionesAutorizadosRed);

                            rta = Datos.ActualizaSalidaTransacciones(transaccionesAutorizadosRed);

                            if (rta.Equals("OK"))
                            {
                                rta = Datos.ActualizaEstadoSincronizacion(transaccionesAutorizadosRedLocal);
                                if (rta.Equals("OK"))
                                {
                                    rta = "OK";
                                }
                                else
                                {
                                    return rta;
                                }
                            }
                            else
                            {
                                return rta;

                            }
                        }

                    }
                    else
                    {
                        rta = Datos.InsertarInformacionNube(transaccionesAutorizadosRed);
                        if (rta.Equals("OK"))
                        {
                            rta = Datos.ActualizaEstadoSincronizacion(transaccionesAutorizadosRedLocal);
                            if (rta.Equals("OK"))
                            {
                                rta = "OK";
                            }
                            else
                            {
                                return rta;
                            }
                        }
                        else
                        {
                            return rta;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                rta = ex.Message;
            }
            return rta;
        }

        #endregion

        #region Bajada
        public static string SincronizarTransaccionesBajada(TransaccionesAutorizadosRed transaccionesAutorizadosRed)
        {
            DataTable tabla;
            string rta = "";
            RepositorioTransaccionesAutorizados Datos = new RepositorioTransaccionesAutorizados();
            TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal = new TransaccionesAutorizadosRedLocal();
            try
            {
                tabla = Datos.ConsultarTransaccionesNube(transaccionesAutorizadosRed);
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow lstDatos in tabla.Rows)
                    {
                        transaccionesAutorizadosRed.IdTransaccion = Convert.ToInt64(lstDatos["IdTransaccion"]);
                        transaccionesAutorizadosRed.CarrilEntrada = Convert.ToInt32(lstDatos["CarrilEntrada"]);
                        transaccionesAutorizadosRed.ModuloEntrada = lstDatos["ModuloEntrada"].ToString();
                        transaccionesAutorizadosRed.IdEstacionamiento = Convert.ToInt64(lstDatos["IdEstacionamiento"]);
                        transaccionesAutorizadosRed.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                        transaccionesAutorizadosRed.PlacaEntrada = lstDatos["PlacaEntrada"].ToString();
                        transaccionesAutorizadosRed.FechaEntrada = Convert.ToDateTime(lstDatos["FechaEntrada"]);
                        transaccionesAutorizadosRed.FechaSalida = lstDatos["FechaSalida"] != DBNull.Value ? Convert.ToDateTime(lstDatos["FechaSalida"]) : (DateTime?)null;
                        transaccionesAutorizadosRed.ModuloSalida = lstDatos["ModuloSalida"].ToString();
                        transaccionesAutorizadosRed.CarrilSalida = lstDatos["CarrilSalida"] != DBNull.Value ? Convert.ToInt32(lstDatos["CarrilSalida"]) : (int?)null;
                        transaccionesAutorizadosRed.PlacaSalida = lstDatos["PlacaSalida"].ToString();
                        transaccionesAutorizadosRed.IdTipoVehiculo = Convert.ToInt32(lstDatos["IdTipoVehiculo"]);
                        transaccionesAutorizadosRed.IdAutorizado = Convert.ToInt32(lstDatos["IdAutorizado"]);
                        transaccionesAutorizadosRed.IdEstacionamiento8 = Convert.ToInt64(lstDatos["IdEstacionamiento8"]);
                        transaccionesAutorizadosRed.IdEstacionamiento9 = Convert.ToInt64(lstDatos["IdEstacionamiento9"]);
                        transaccionesAutorizadosRed.IdEstacionamiento10 = Convert.ToInt64(lstDatos["IdEstacionamiento10"]);
                        transaccionesAutorizadosRed.IdEstacionamiento11 = Convert.ToInt64(lstDatos["IdEstacionamiento11"]);
                        transaccionesAutorizadosRed.IdEstacionamiento12 = Convert.ToInt64(lstDatos["IdEstacionamiento12"]);
                    }

                    Mapeo(transaccionesAutorizadosRed, transaccionesAutorizadosRedLocal);

                    tabla = Datos.ValidarSiExisteTransaccionLocal(transaccionesAutorizadosRedLocal);
                    if (tabla.Rows.Count > 0)
                    {
                        foreach (DataRow lstDatos in tabla.Rows)
                        {

                            transaccionesAutorizadosRedLocal.FechaSalida = lstDatos["FechaSalida"] != DBNull.Value ? Convert.ToDateTime(lstDatos["FechaSalida"]) : (DateTime?)null;
                            transaccionesAutorizadosRedLocal.ModuloSalida = lstDatos["ModuloSalida"].ToString();
                            transaccionesAutorizadosRedLocal.CarrilSalida = lstDatos["CarrilSalida"] != DBNull.Value ? Convert.ToInt32(lstDatos["CarrilSalida"]) : (int?)null;
                            transaccionesAutorizadosRedLocal.PlacaSalida = lstDatos["PlacaSalida"].ToString();
                        }
                        if (transaccionesAutorizadosRed.ModuloSalida == transaccionesAutorizadosRedLocal.ModuloSalida)
                        {
                            rta = Datos.ActualizaEstadoSincronizacionNube(transaccionesAutorizadosRed);
                            if (rta.Equals("OK"))
                            {
                                rta = "OK";
                            }
                            else
                            {
                                return rta;
                            }
                        }
                        else
                        {
                    Mapeo(transaccionesAutorizadosRed, transaccionesAutorizadosRedLocal);

                            rta = Datos.ActualizaSalidaTransaccionesLocal(transaccionesAutorizadosRedLocal);

                            if (rta.Equals("OK"))
                            {
                                rta = Datos.ActualizaEstadoSincronizacionNube(transaccionesAutorizadosRed);
                                if (rta.Equals("OK"))
                                {
                                    rta = "OK";
                                }
                                else
                                {
                                    return rta;
                                }
                            }
                            else
                            {
                                return rta;

                            }
                        }

                    }

                    else
                    {
                        rta = Datos.InsertarInformacionLocal(transaccionesAutorizadosRedLocal);
                        if (rta.Equals("OK"))
                        {
                            rta = Datos.ActualizaEstadoSincronizacionNube(transaccionesAutorizadosRed);
                            if (rta.Equals("OK"))
                            {
                                rta = "OK";
                            }
                            else
                            {
                                return rta;
                            }
                        }
                        else
                        {
                            return rta;
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                rta = ex.ToString();
            }
            return rta;

        }
        #endregion

        public static void Mapeo(object origen, object destino)
        {
            var propiedadesOrigen = origen.GetType().GetProperties();
            var propiedadesDestino = destino.GetType().GetProperties();

            foreach (var propiedadDestino in propiedadesDestino)
            {
                var propiedadOrigen = propiedadesOrigen.FirstOrDefault(p => p.Name == propiedadDestino.Name && p.PropertyType == propiedadDestino.PropertyType);

                if (propiedadOrigen != null)
                {
                    var valor = propiedadOrigen.GetValue(origen);
                    propiedadDestino.SetValue(destino, valor);
                }
            }
        }
    }
}
