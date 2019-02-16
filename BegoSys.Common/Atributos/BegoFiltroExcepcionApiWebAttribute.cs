#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio medina
// Fecha de Creación            : 2018/12/17
// Modificado Por               : mauricio medina
// Fecha Modificación           : 2018/12/17
// Empresa                      : Bego Inversiones S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Excepciones;
using BegoSys.Common.Auxiliares;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;
#endregion

namespace BegoSys.Common.Atributos
{
    /// <summary>
    /// Atributo que permite interceptar el método cuando occure una excepción no controlada.
    /// </summary>
    public class BegoFiltroExcepcionApiWebAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Sobreescribe el método para controlar las excepciones que se generan desde los web api controllers de la capa de servicios api.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            bool isNotFoundException = false;

            if (isNotFoundException = context.Exception is ObjetoNoEncontradoException || context.Exception is ObjetoExistenteException)
            {
                HttpStatusCode codigoResultado = isNotFoundException ? HttpStatusCode.NotFound : HttpStatusCode.Conflict;

                string llaveMensajeDescripcion = isNotFoundException ? "SGDHMError_ObjetoNoEncontradoException" : "SGDHMError_ObjetoExistenteException";

                string descripcionResultado = AuxiliarBegoSys.TraducirMensaje(llaveMensajeDescripcion);

                context.Response = context.Request.CreateResponse(codigoResultado, descripcionResultado);
            }
            else
            {
                var exception = context.Exception as BegoSysException;

                if (exception == null)
                {
                    //Excepción no controlada
                    var controllerName = context.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    var actionName = context.ActionContext.ActionDescriptor.ActionName;

                    exception = new BegoSysException("BegoSysError_ExcepcionNoControlada", context.Exception, actionName, controllerName);
                }

                AuxiliarBegoSys.EscribirError(exception.Message, exception);

                HttpError httpError = new HttpError
                {
                    { "status", "error" },

                    { "message", context.Exception.Message },

                    { "inner_exceptions", ErrorJsonGenerico.ObtenerExcepcionesAnidadas(context.Exception) },

                    { "stack_trace", context.Exception.StackTrace }
                };

                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, httpError);
            }
        }
    }
}
