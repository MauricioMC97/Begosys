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
using BegoSys.Common.Constantes;
using BegoSys.Common.ProveedoresDependencias;
using Spring.Rest.Client;
using System;
using System.Web.Mvc;
#endregion

namespace BegoSys.Web.Models
{
    /// <summary>
    /// Atributo personalizado que permite interceptar solicitudes a la API para ejecutar
    /// acciones previas o posteriores al llamado
    /// </summary>
    public class BegoSysAuditHttpAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        ///// <summary>
        ///// Manejador del evento que se dispara antes de que se ejecute una acción
        ///// </summary>
        ///// <param name="actionContext"></param>
        //public override void OnActionExecuting(ActionExecutingContext actionContext)
        //{
        //    // Stores the Request in an Accessible object  
        //    var request = actionContext.HttpContext.Request;
        //    // Generate an audit  
        //    AuditoriaTo audit = new AuditoriaTo()
        //    {
        //        // Your Audit Identifier   
        //        AuditID = Guid.NewGuid(),
        //        // Our Username (if available)  
        //        UsuarioRed = (request.IsAuthenticated) ? actionContext.HttpContext.User.Identity.Name : "Anonymous",
        //        // The IP Address of the Request  
        //        IPAddress = string.IsNullOrEmpty(request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? request.UserHostAddress : request.ServerVariables["HTTP_X_FORWARDED_FOR"],
        //        // The URL that was accessed  
        //        Opcion = request.RawUrl,
        //        // Creates our Timestamp  
        //        Fecha = DateTime.UtcNow
        //    };
        //    AuditoriaRepository _repositorioAuditoria = new AuditoriaRepository();

        //    RestTemplate _proxy = LocalizadorServicioSgdhm<RestTemplate>.GetService();
        //    var variables = _proxy.PostForMessage(ConstantesApi.RegistrarAuditoriaUri, audit);
        //    // Stores the Audit in the Database  
        //    //var respuesta = _repositorioAuditoria.RegistrarAuditoriaAsync(audit);
        //    // Finishes executing the Action as normal   
        //    base.OnActionExecuting(actionContext);
        //}
    }
}
