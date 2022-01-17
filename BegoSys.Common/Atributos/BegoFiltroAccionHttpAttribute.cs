#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio medina
// Fecha de Creación            : 2018/12/17
// Modificado Por               : mauricio medina
// Fecha Modificación           : 2018/12/17
// Empresa                      : BEGO INVERSIONES S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Auxiliares;
using BegoSys.Common.Constantes;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
#endregion

namespace BegoSys.Common.Atributos
{
    /// <summary>
    /// Atributo personalizado que permite interceptar solicitudes a la API para ejecutar
    /// acciones previas o posteriores al llamado
    /// </summary>
    public class BegoFiltroAccionHttpAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Manejador del evento que se dispara antes de que se ejecute una acción
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //pre-processing
            //Se sobreescribe la localización del hilo de ejecución
            AuxiliarBegoSys.ActualizarCulturaActual();

            //Se hace registro en el log
            var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = actionContext.ActionDescriptor.ActionName;
            var startDateTime = DateTime.Now;
            AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "BegoSysDebug_RegistroInicioMetodo", actionName, controllerName, startDateTime.ToShortDateString(), startDateTime.ToShortTimeString());
        }

        /// <summary>
        /// Manejador del evento que se dispara despues de ejecutada la acción
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //Post-processing
            //Se hace registro en el log
            var controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            var endDateTime = DateTime.Now;
            AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "BegoSysDebug_RegistroFinMetodo", actionName, controllerName, endDateTime.ToShortDateString(), endDateTime.ToShortTimeString());
        }
    }
}
