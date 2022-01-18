#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/12/16
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2018/12/16
// Empresa                      : BEGO INVERSIONES SAS
// ===================================================
#endregion

using BegoSys.Common.ProveedoresDependencias;
using Spring.Context;
using Spring.Context.Support;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BegoSys.Core;
using System;
using BegoSys.Common.Auxiliares;
using BegoSys.Common.Constantes;

namespace BegoSys.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            ServiceStack.Logging.LogManager.LogFactory = new ServiceStack.Logging.Log4Net.Log4NetFactory();
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("Startup application.");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Custom dependency resolver
            //Se registra el encargado de resolver las instancias y dependencias
            IApplicationContext context = ContextRegistry.GetContext();
            DependencyResolver.SetResolver(new SolucionadorDependenciaBegoSystem(context));
        }

        void Session_End(object sender, EventArgs e)
        {
            System.Guid id = Guid.NewGuid();

            AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "Session_End , id: " + id.ToString(),
                                          DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            Session.Abandon();
        }
    }
}
