#region Derechos Reservados
// ===================================================
// Desarrollado Por             : robert.medina
// Fecha de Creación            : 2017/07/25
// Modificado Por               : robert.medina
// Fecha Modificación           : 2017/07/25
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

namespace BegoSys.Common.Auxiliares
{
    using Newtonsoft.Json;
    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// A <see cref="JsonResult"/> implementation that uses JSON.NET to perform the serialization.
    /// </summary>
    public class ResultadoJsonNet : JsonResult
    {
        public ResultadoJsonNet()
        {
            Formatting = Formatting.None;
            SerializerSettings = new JsonSerializerSettings();
        }

        public Formatting Formatting { get; set; }

        public JsonSerializerSettings SerializerSettings { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                using (JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting })
                {
                    JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);

                    serializer.Serialize(writer, Data);

                    writer.Flush();
                }
            }
        }
    }
}

