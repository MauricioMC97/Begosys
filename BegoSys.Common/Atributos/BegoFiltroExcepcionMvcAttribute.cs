#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/12/17
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2018/12/17
// Empresa                      : bego inversiones S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Excepciones;
using BegoSys.Common.Auxiliares;
using Newtonsoft.Json;
using Spring.Rest.Client;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
#endregion

namespace BegoSys.Common.Atributos
{
    /// <summary>
    /// Atributo personalizado que permite interceptar una excepción que es arrojada por 
    /// una acción
    /// </summary>
    public class BegoFiltroExcepcionMvcAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Sobreescribe el método para controlar las excepciones que se generan desde los controladores de IDEAM.SGDHM.Web
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.Exception is HttpClientErrorException)
            {
                HttpClientErrorException excepcionCliente = (HttpClientErrorException)filterContext.Exception;

                filterContext.Result = new HttpStatusCodeResult(excepcionCliente.Response.StatusCode, excepcionCliente.Response.StatusDescription);
            }

            if (filterContext.ExceptionHandled || filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            var exception = filterContext.Exception as BegoSysException;

            if (exception == null)
            {
                exception = new BegoSysException("BegoSysError_ExcepcionNoControlada", filterContext.Exception, actionName, controllerName);
            }

            //Se registra en el log el detalle de la excepción
            AuxiliarBegoSys.EscribirError(exception.Message, exception);

            if (filterContext.RequestContext.HttpContext.Request.Headers.AllKeys.Contains("X-Requested-With") && filterContext.RequestContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                ErrorJsonGenerico errorObject = null;

                if (filterContext.Exception is HttpResponseException)
                {
                    HttpResponseException ex = (HttpResponseException)filterContext.Exception;

                    string errorJson = ex.GetResponseBodyAsString();

                    try
                    {
                        errorObject = JsonConvert.DeserializeObject<ErrorJsonGenerico>(errorJson);
                    }
                    catch (Exception)
                    {
                        errorObject = new ErrorJsonGenerico()
                        {
                            Mensaje = errorJson
                        };
                    }
                }
                else
                {
                    errorObject = ErrorJsonGenerico.DeExcepcion(filterContext.Exception);
                }

                filterContext.ExceptionHandled = true;

                filterContext.Result = new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(errorObject),
                    ContentType = "application/json"
                };

                return;
            }

#if !DEBUG
            HandleErrorInfo model = new HandleErrorInfo(exception, controllerName, actionName);
            filterContext.Result = new ViewResult
            {
                ViewName = View,
                MasterName = Master,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
#endif
        }
    }
}

