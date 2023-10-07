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
    public class PersonasAutorizadasController
    {
        #region Subida
        public static string SincronizarPersonasAutorizadasSubida()
        {
            DataTable tabla;
            RepositorioPersonasAutorizadas Datos = new RepositorioPersonasAutorizadas();
            string rta = "";
            PersonasAutorizadasLocal personasAutorizadasLocal = new PersonasAutorizadasLocal();
            PersonasAutorizadasNube personasAutorizadasNube = new PersonasAutorizadasNube();
            try
            {
                tabla = Datos.ConsultarInformacionPersonasAutorizadaslocal();
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow lstDatos in tabla.Rows)
                    {
                        personasAutorizadasLocal.Documento = lstDatos["Documento"].ToString();
                        personasAutorizadasLocal.IdAutorizacion = Convert.ToInt32(lstDatos["IdAutorizacion"]);
                        personasAutorizadasLocal.NombreApellidos = lstDatos["NombreApellidos"].ToString();
                        personasAutorizadasLocal.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                        personasAutorizadasLocal.FechaCreacion = Convert.ToDateTime(lstDatos["FechaCreacion"]);
                        personasAutorizadasLocal.DocumentoUsuarioCreacion = Convert.ToInt32(lstDatos["DocumentoUsuarioCreacion"]);
                        personasAutorizadasLocal.Estado = Convert.ToBoolean(lstDatos["Estado"]);
                        personasAutorizadasLocal.FechaInicio = Convert.ToDateTime(lstDatos["FechaInicio"]);
                        personasAutorizadasLocal.FechaFin = Convert.ToDateTime(lstDatos["FechaFin"]);
                        personasAutorizadasLocal.Telefono = lstDatos["Telefono"].ToString();
                        personasAutorizadasLocal.Email = lstDatos["Email"].ToString();
                        personasAutorizadasLocal.Placa1 = lstDatos["Placa1"].ToString();
                        personasAutorizadasLocal.Placa2 = lstDatos["Placa2"].ToString();
                        personasAutorizadasLocal.Placa3 = lstDatos["Placa3"].ToString();
                        personasAutorizadasLocal.Placa4 = lstDatos["Placa4"].ToString();
                        personasAutorizadasLocal.Placa5 = lstDatos["Placa5"].ToString();
                        personasAutorizadasLocal.Sincronizacion = Convert.ToBoolean(lstDatos["Sincronizacion"]);
                    }

                    Mapeo(personasAutorizadasLocal, personasAutorizadasNube);
                    personasAutorizadasNube.IdEstacionamiento8 = 0;
                    personasAutorizadasNube.IdEstacionamiento9 = 0;
                    personasAutorizadasNube.IdEstacionamiento10 = 0;
                    personasAutorizadasNube.IdEstacionamiento11 = 0;
                    personasAutorizadasNube.IdEstacionamiento12 = 1;
                    personasAutorizadasNube.EstadoNew = Convert.ToBoolean(personasAutorizadasNube.Estado) ? 1 : 0;


                    tabla = Datos.ConsultarInformacionPersonasAutorizadasNube(personasAutorizadasNube);

                    if (tabla.Rows.Count > 0)
                    {

                        int resultadoFechaInicio = DateTime.Compare(personasAutorizadasLocal.FechaInicio, personasAutorizadasNube.FechaInicio);
                        int resultadoFechaFin = DateTime.Compare(personasAutorizadasLocal.FechaFin, personasAutorizadasNube.FechaFin);

                        int resultadoFinal = resultadoFechaInicio + resultadoFechaFin;

                        // si Final es menor que 0 quiere decir que la fecha local es menor que la de la nube
                        // si Final es igual que 0 quiere decir que la fecha local es igual que la de la nube
                        // si Final es mayor que 0 quiere decir que la fecha local es mayor que la de la nube

                        if (resultadoFinal > 0)
                        {
                            rta = Datos.ActualizaPersonasAutorizadasNube(personasAutorizadasNube);
                            if (rta.Equals("OK"))
                            {
                                rta = Datos.ActualizaSincronizacionPersonasAutorizadasLocal(personasAutorizadasLocal);
                                if (rta.Equals("OK"))
                                {
                                    rta = "OK";
                                }
                            }
                        }
                        else if (resultadoFinal < 0)
                        {
                            rta = Datos.ActualizaPersonasAutorizadasNube(personasAutorizadasNube);
                            if (rta.Equals("OK"))
                            {
                                rta = Datos.ActualizaSincronizacionPersonasAutorizadasLocal(personasAutorizadasLocal);
                                if (rta.Equals("OK"))
                                {
                                    rta = "OK";
                                }
                            }
                        }
                        else if (resultadoFinal == 0)
                        {
                            rta = Datos.ActualizaPersonasAutorizadasNube(personasAutorizadasNube);
                            if (rta.Equals("OK"))
                            {
                                rta = Datos.ActualizaSincronizacionPersonasAutorizadasLocal(personasAutorizadasLocal);
                                if (rta.Equals("OK"))
                                {
                                    rta = "OK";
                                }
                            }
                        }

                    }
                    else
                    {
                        rta = Datos.InsertaInformacionNube(personasAutorizadasNube);
                        if (rta.Equals("OK"))
                        {
                            rta = Datos.ActualizaSincronizacionPersonasAutorizadasLocal(personasAutorizadasLocal);
                            if (rta.Equals("OK"))
                            {
                                rta = "OK";
                            }
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

        #region Bajada
        public static string SincronizarPersonasAutorizadasBajada()
        {
            DataTable tabla;
            RepositorioPersonasAutorizadas Datos = new RepositorioPersonasAutorizadas();
            string rta = "";
            PersonasAutorizadasLocal personasAutorizadasLocal = new PersonasAutorizadasLocal();
            PersonasAutorizadasNube personasAutorizadasNube = new PersonasAutorizadasNube();
            Tarjetas tarjetas = new Tarjetas();
            try

            {
                tabla = Datos.ConsultaInformacionPersonasAutorizadasNubeBajada();
                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow lstDatos in tabla.Rows)
                    {

                        personasAutorizadasNube.Documento = lstDatos["Documento"].ToString();
                        personasAutorizadasNube.IdAutorizacion = Convert.ToInt32(lstDatos["IdAutorizacion"]);
                        personasAutorizadasNube.NombreApellidos = lstDatos["NombreApellidos"].ToString();
                        personasAutorizadasNube.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                        personasAutorizadasNube.FechaCreacion = Convert.ToDateTime(lstDatos["FechaCreacion"]);
                        personasAutorizadasNube.DocumentoUsuarioCreacion = Convert.ToInt32(lstDatos["DocumentoUsuarioCreacion"]);
                        personasAutorizadasNube.Estado = Convert.ToBoolean(lstDatos["Estado"]);
                        personasAutorizadasNube.FechaInicio = Convert.ToDateTime(lstDatos["FechaInicio"]);
                        personasAutorizadasNube.FechaFin = Convert.ToDateTime(lstDatos["FechaFin"]);
                        personasAutorizadasNube.Telefono = lstDatos["Telefono"].ToString();
                        personasAutorizadasNube.Email = lstDatos["Email"].ToString();
                        personasAutorizadasNube.Placa1 = lstDatos["Placa1"].ToString();
                        personasAutorizadasNube.Placa2 = lstDatos["Placa2"].ToString();
                        personasAutorizadasNube.Placa3 = lstDatos["Placa3"].ToString();
                        personasAutorizadasNube.Placa4 = lstDatos["Placa4"].ToString();
                        personasAutorizadasNube.Placa5 = lstDatos["Placa5"].ToString();
                        personasAutorizadasNube.IdEstacionamiento8 = Convert.ToInt64(lstDatos["IdEstacionamiento8"]);
                        personasAutorizadasNube.IdEstacionamiento9 = Convert.ToInt64(lstDatos["IdEstacionamiento9"]);
                        personasAutorizadasNube.IdEstacionamiento10 = Convert.ToInt64(lstDatos["IdEstacionamiento10"]);
                        personasAutorizadasNube.IdEstacionamiento11 = Convert.ToInt64(lstDatos["IdEstacionamiento11"]);
                        personasAutorizadasNube.IdEstacionamiento12 = Convert.ToInt64(lstDatos["IdEstacionamiento12"]);

                    }
                    Mapeo(personasAutorizadasNube, personasAutorizadasLocal);
                    Mapeo(personasAutorizadasNube, tarjetas);

                    tabla = Datos.ConsultarInformacionPersonasAutorizadasLocalBajada(personasAutorizadasLocal);
                    if (tabla.Rows.Count > 0)
                    {
                        int resultadoFechaInicio = DateTime.Compare(personasAutorizadasLocal.FechaInicio, personasAutorizadasNube.FechaInicio);
                        int resultadoFechaFin = DateTime.Compare(personasAutorizadasLocal.FechaFin, personasAutorizadasNube.FechaFin);

                        int resultadoFinal = resultadoFechaInicio + resultadoFechaFin;


                        // si Final es menor que 0 quiere decir que la fecha nube es menor que la de la local
                        // si Final es igual que 0 quiere decir que la fecha nube es igual que la de la local
                        // si Final es mayor que 0 quiere decir que la fecha nube es mayor que la de la local
                        if (resultadoFinal > 0)
                        {
                            tabla = Datos.ValidarTarjetInventarioBajada(personasAutorizadasLocal);
                            if (tabla.Rows.Count > 0)
                            {
                                foreach (DataRow lstDatos in tabla.Rows)
                                {
                                    tarjetas.IdEstacionamiento = Convert.ToInt64(lstDatos["IdEstacionamiento"]);
                                    tarjetas.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                                    tarjetas.FechaRegistro = Convert.ToDateTime(lstDatos["FechaRegistro"]);
                                    tarjetas.DocumentoUsuarioRegistro = lstDatos["DocumentoUsuarioRegistro"].ToString();
                                    tarjetas.Estado = Convert.ToBoolean(lstDatos["Estado"]);

                                }

                                if (tarjetas.Estado == false)
                                {
                                    rta = Datos.ActualizaEstadoTarjeta(tarjetas);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                rta = Datos.InsertaTarjetaLocalBajada(tarjetas);
                                if (rta.Equals("OK"))
                                {
                                    rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (resultadoFinal == 0)
                        {
                            tabla = Datos.ValidarTarjetInventarioBajada(personasAutorizadasLocal);
                            if (tabla.Rows.Count > 0)
                            {
                                foreach (DataRow lstDatos in tabla.Rows)
                                {
                                    tarjetas.IdEstacionamiento = Convert.ToInt64(lstDatos["IdEstacionamiento"]);
                                    tarjetas.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                                    tarjetas.FechaRegistro = Convert.ToDateTime(lstDatos["FechaRegistro"]);
                                    tarjetas.DocumentoUsuarioRegistro = lstDatos["DocumentoUsuarioRegistro"].ToString();
                                    tarjetas.Estado = Convert.ToBoolean(lstDatos["Estado"]);

                                }

                                if (tarjetas.Estado == false)
                                {
                                    rta = Datos.ActualizaEstadoTarjeta(tarjetas);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                                if (rta.Equals("OK"))
                                                {
                                                    rta = "OK";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                rta = Datos.InsertaTarjetaLocalBajada(tarjetas);
                                if (rta.Equals("OK"))
                                {
                                    rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (resultadoFinal < 0)
                        {
                            tabla = Datos.ValidarTarjetInventarioBajada(personasAutorizadasLocal);
                            if (tabla.Rows.Count > 0)
                            {
                                foreach (DataRow lstDatos in tabla.Rows)
                                {
                                    tarjetas.IdEstacionamiento = Convert.ToInt64(lstDatos["IdEstacionamiento"]);
                                    tarjetas.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                                    tarjetas.FechaRegistro = Convert.ToDateTime(lstDatos["FechaRegistro"]);
                                    tarjetas.DocumentoUsuarioRegistro = lstDatos["DocumentoUsuarioRegistro"].ToString();
                                    tarjetas.Estado = Convert.ToBoolean(lstDatos["Estado"]);

                                }

                                if (tarjetas.Estado == false)
                                {
                                    rta = Datos.ActualizaEstadoTarjeta(tarjetas);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                                if (rta.Equals("OK"))
                                                {
                                                    rta = "OK";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                rta = Datos.InsertaTarjetaLocalBajada(tarjetas);
                                if (rta.Equals("OK"))
                                {
                                    rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        rta = Datos.InsertaInformacionLocal(personasAutorizadasNube);
                        if (rta.Equals("OK"))
                        {
                            tabla = Datos.ValidarTarjetInventarioBajada(personasAutorizadasLocal);
                            if (tabla.Rows.Count > 0)
                            {
                                foreach (DataRow lstDatos in tabla.Rows)
                                {
                                    tarjetas.IdEstacionamiento = Convert.ToInt64(lstDatos["IdEstacionamiento"]);
                                    tarjetas.IdTarjeta = lstDatos["IdTarjeta"].ToString();
                                    tarjetas.FechaRegistro = Convert.ToDateTime(lstDatos["FechaRegistro"]);
                                    tarjetas.DocumentoUsuarioRegistro = lstDatos["DocumentoUsuarioRegistro"].ToString();
                                    tarjetas.Estado = Convert.ToBoolean(lstDatos["Estado"]);

                                }

                                if (tarjetas.Estado == false)
                                {
                                    rta = Datos.ActualizaEstadoTarjeta(tarjetas);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                                if (rta.Equals("OK"))
                                                {
                                                    rta = "OK";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                   rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                   if (rta.Equals("OK"))
                                   {
                                        rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = "OK";
                                        }
                                    }
 
                                }
                            }
                            else
                            {
                                rta = Datos.InsertaTarjetaLocalBajada(tarjetas);
                                if (rta.Equals("OK"))
                                {
                                    rta = Datos.ActualizarInformacionLocalBajada(personasAutorizadasLocal);
                                    if (rta.Equals("OK"))
                                    {
                                        rta = Datos.ActualizaSincronizacionRedUnab(personasAutorizadasLocal);
                                        if (rta.Equals("OK"))
                                        {
                                            rta = Datos.ActualizaSincronizacionNube(personasAutorizadasNube);
                                            if (rta.Equals("OK"))
                                            {
                                                rta = "OK";
                                            }
                                        }
                                    }
                                }
                            }
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
