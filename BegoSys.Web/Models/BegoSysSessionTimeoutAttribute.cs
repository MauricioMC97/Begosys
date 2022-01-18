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

using BegoSys.Common.Auxiliares;
using BegoSys.Common.Constantes;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BegoSys.Web.Models
{
    /// <summary>
    /// Atributo personalizado que permite interceptar solicitudes a la API para ejecutar
    /// acciones previas o posteriores al llamado
    /// </summary>
    public class SGDHMSessionTimeoutAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Guid id = Guid.NewGuid();

            AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "Inicio SGDHMSessionTimeoutAttribute SessionId " + (string.IsNullOrEmpty(AuxiliarSesion.Current.SessionId) ? "Error" : AuxiliarSesion.Current.SessionId) + ", id: " + id.ToString(),
                                          DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());

            var SessionId = AuxiliarSesion.Current.SessionId;
            if (SessionId == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "EndSessionHome",
                    controller = "Home"
                }));
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}


