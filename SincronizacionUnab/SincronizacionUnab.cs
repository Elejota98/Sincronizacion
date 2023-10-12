using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Controlador;
using Modelo;

namespace SincronizacionUnab
{
    public partial class SincronizacionUnab : ServiceBase
    {

        #region Definiciones
        private static object objLock = new object();
        private static SincronizacionUnab Agente = new SincronizacionUnab();
        public string rta = string.Empty;
        private Timer oTimer;
        TransaccionesAutorizadosRed transaccionesAutorizadosRed = new TransaccionesAutorizadosRed();
        TransaccionesAutorizadosRedLocal transaccionesAutorizadosRedLocal = new TransaccionesAutorizadosRedLocal();
        FacturasContingencia facturasContingencia = new FacturasContingencia();

        private static int _PeriodoEjecucionSegundos
        {
            get
            {
                string sPeriodoEjecucionSegundos = ConfigurationManager.AppSettings["PeriodoEjecucionSegundos"];
                if (string.IsNullOrEmpty(sPeriodoEjecucionSegundos))
                {
                    return 10;
                }
                else
                {
                    return Convert.ToInt32(sPeriodoEjecucionSegundos);
                }
            }
        }

        private static int _IdEstacionamiento
        {
            get
            {
                string sPeriodoEjecucionSegundos = ConfigurationManager.AppSettings["IdEstacionamiento"];
                if (string.IsNullOrEmpty(sPeriodoEjecucionSegundos))
                {
                    return 10;
                }
                else
                {
                    return Convert.ToInt32(sPeriodoEjecucionSegundos);
                }
            }
        }

        #endregion

        public SincronizacionUnab()
        {
            oTimer = new Timer(2 * 1000);
            oTimer.Elapsed += new ElapsedEventHandler(oTimer_Elapsed);
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            oTimer.Enabled = true;

        }

        protected override void OnStop()
        {
            oTimer.Enabled = false;

        }

        void oTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                oTimer.Enabled = false;

                if (Environment.UserInteractive)
                    Console.WriteLine("El servicio inicia revision");

                lock (objLock)
                {
                    transaccionesAutorizadosRed.IdEstacionamiento = _IdEstacionamiento;
                    transaccionesAutorizadosRedLocal.IdEstacionamiento = _IdEstacionamiento;
                    facturasContingencia.IdEstacionamiento = _IdEstacionamiento;

                    PersonasAutorizadasController.SincronizarPersonasAutorizadasSubida();
                    PersonasAutorizadasController.SincronizarPersonasAutorizadasBajada();

                    rta =  ClientesController.SincronizarClientes();
                    if (rta.Equals("OK"))
                    {

                    }

                    TransaccionesAutorizadosController.SincronizarTransaccionesSubida(transaccionesAutorizadosRedLocal);
                    TransaccionesAutorizadosController.SincronizarTransaccionesBajada(transaccionesAutorizadosRed);

                     rta = PagosFEController.SincronizaPagosFE();
                    if (rta.Equals("OK"))
                    {

                    }

                    if (PagosContingenciaController.SincronizarPagosContingencia(facturasContingencia))
                    {
                    }



                    oTimer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                string sFechaFile = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
                //TraceHandler.WriteLine(LOG.NombreArchivoLogRegistraArchivos + sFechaFile, "SERVICIO WINDOWS: excepcion servicio: " + ex.Source + " " + ex.StackTrace + " " + ex.Message, TipoLog.TRAZA);

                oTimer.Enabled = true;
            }
        }
        static void Main(string[] args)
        {
            Process.GetCurrentProcess().Exited += new EventHandler(SincronizacionUnab_Exited);

            try
            {
                if (System.Diagnostics.Process.GetProcessesByName
                    (System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
                    throw new ApplicationException("Existe otra instancia del servicio en ejecución.");

                if (!Environment.UserInteractive)
                    SincronizacionUnab.Run(Agente);
                else
                {
                    if (Environment.UserInteractive)
                        Console.ForegroundColor = ConsoleColor.Green;

                    Agente.OnStart(null);

                    if (Environment.UserInteractive)
                        Console.ForegroundColor = ConsoleColor.Green;

                    if (Environment.UserInteractive)
                        Console.WriteLine("El servicio se inicio correctamente y queda en espera de atender solicitudes.");

                    System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                }
            }
            catch (Exception ex)
            {
                if (Environment.UserInteractive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ocurrió un error iniciando el servicio.");
                    Console.WriteLine(ex.Message);
                    System.Threading.Thread.Sleep(new TimeSpan(0, 1, 0));
                }
            }
        }

        static void SincronizacionUnab_Exited(object sender, EventArgs e)
        {
            try
            {
                if (!Environment.UserInteractive)
                {
                    Agente.OnStop();
                    Agente = null;
                }
            }
            catch { }
        }


    }
}
