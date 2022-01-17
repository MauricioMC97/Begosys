using BegoSys.Common;

#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio.medina
// Fecha de Creación            : 2017/07/28
// Modificado Por               : robert.medina
// Fecha Modificación           : 2017/07/28
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

namespace BegoSys.Common.Auxiliares
{
    using System;
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// Contiene métodos de utilidad para los controladores mvc.
    /// </summary>
    public class ControladorJsonNet : Controller
    {
        /// <summary>
        /// Devuelve una respuesta indicando que el proceso finalizó existosamente.
        /// </summary>
        /// <param name="controller">controlador al que se aplica el método.</param>
        /// <returns>action result con la respuesta.</returns>
        public ResultadoJsonNet Ok()
        {
            return new ResultadoJsonNet()
            {
                Data = new RespuestaJsonGenerica<object>()
                {
                    Resultado = null
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Devuelve una respuesta indicando que el proceso finalizó existosamente.
        /// </summary>
        /// <typeparam name="T">tipo de la respuesta.</typeparam>
        /// <param name="controller">controlador al que se aplica el método.</param>
        /// <param name="result"></param>
        /// <returns>action result con la respuesta.</returns>
        public ResultadoJsonNet Ok<T>(T result)
        {
            return new ResultadoJsonNet()
            {
                Data = new RespuestaJsonGenerica<T>()
                {
                    Resultado = result
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Devuelve una respuesta indicando que ocurrió un error en el proceso.
        /// </summary>
        /// <param name="controller">controlador al que se aplica el método.</param>
        /// <param name="exception">excepción a devolver.</param>
        /// <returns>action result con la respuesta.</returns>
        public ResultadoJsonNet Error(Exception exception)
        {
            return new ResultadoJsonNet()
            {
                Data = ErrorJsonGenerico.DeExcepcion(exception),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Devuelve una respuesta indicando que ocurrió un error en el proceso.
        /// </summary>
        /// <param name="controller">controlador al que se aplica el método.</param>
        /// <param name="message">mensaje con el error ocurrido.</param>
        /// <returns>action result con la respuesta.</returns>
        public ResultadoJsonNet Error(string message)
        {
            return new ResultadoJsonNet()
            {
                Data = new ErrorJsonGenerico()
                {
                    Mensaje = message
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected internal new JsonResult Json(object data)
        {
            return new ResultadoJsonNet()
            {
                Data = data
            };
        }

        protected internal new JsonResult Json(object data, string contentType)
        {
            return new ResultadoJsonNet()
            {
                Data = data,
                ContentType = contentType
            };
        }


        protected internal virtual new JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new ResultadoJsonNet()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected internal new JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            return new ResultadoJsonNet()
            {
                Data = data,
                JsonRequestBehavior = behavior
            };
        }

        protected internal new JsonResult Json(object data, string contentType, JsonRequestBehavior behavior)
        {
            return new ResultadoJsonNet()
            {
                Data = data,
                ContentType = contentType,
                JsonRequestBehavior = behavior
            };
        }

        protected internal virtual new JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ResultadoJsonNet()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}