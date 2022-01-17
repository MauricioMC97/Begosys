#region Copyright
/*
 * Created by:      mauricio medina
 * Created date:    2018/12/16
 * Modified by:     2018/12/16
 * Modified date:   mauricio medina
 * Company:         Bego Inversiones S.A.S
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BegoSys.Common.Auxiliares;
using BegoSys.Common.Constantes;

namespace BegoSys.Common.Atributos
{
    /// <summary>
    /// Filtro personalizado para interceptar los mensajes de los métodos que se invocan en los Web API
    /// </summary>
    public class BegoFiltroAccionMvcAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Manejador del evento que se dispara antes de que se ejecute una acción
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //pre-processing
            //Se sobreescribe la localización del hilo de ejecución
            AuxiliarBegoSys.ActualizarCulturaActual();

            //Se hace registro en el log
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            DateTime startDateTime = DateTime.Now;
            AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "BegoSysDebug_RegistroInicioMetodo", actionName, controllerName, startDateTime.ToShortDateString(), startDateTime.ToShortTimeString());
        }

        /// <summary>
        /// Manejador del evento que se dispara despues de ejecutada la acción
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Post-processing
            //Se hace registro en el log
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            DateTime endDateTime = DateTime.Now;
            AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "BegoSysDebug_RegistroFinMetodo", actionName, controllerName, endDateTime.ToShortDateString(), endDateTime.ToShortTimeString());
        }
    }
}
