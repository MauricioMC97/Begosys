#region Derechos Reservados
// ===================================================
// Desarrollado Por             : bernardo.bustamante
// Fecha de Creación            : 2018/02/10
// Modificado Por               : bernardo.bustamante
// Fecha Modificación           : 2018/02/10
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

#region Referencias
#endregion

using System.Configuration;
using System.Web.Mvc;

namespace BegoSys.Web.Models
{
    public class BegoSysTimeoutFilter : ActionFilterAttribute
    {
        internal const string TimeoutSecondsSettingsKey = "TimeoutSecondsSettingsKey";
        internal static int TimeoutSeconds;
        public BegoSysTimeoutFilter()
        {
            TimeoutSeconds = int.Parse(ConfigurationManager.AppSettings[TimeoutSecondsSettingsKey]);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Web.HttpContext.Current.GetType().GetField("_timeoutState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(System.Web.HttpContext.Current, 1);
            filterContext.Controller.ControllerContext.HttpContext.Server.ScriptTimeout = TimeoutSeconds;
            base.OnActionExecuting(filterContext);
        }
    }
}


