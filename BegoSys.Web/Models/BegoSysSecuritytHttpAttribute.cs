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

using BegoSys.Common.Constantes;
using BegoSys.Web.Enum;
using System.Linq;
using System.Web.Mvc;

namespace BegoSys.Web.Models
{
    /// <summary>
    /// Atributo personalizado que permite interceptar solicitudes a la API para ejecutar
    /// acciones previas o posteriores al llamado
    /// </summary>
    public class BegoSysSecuritytHttpAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private Role[] _roles;
        public BegoSysSecuritytHttpAttribute(params Role[] roles)
        {
            _roles = roles;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool IsInRole = false;
            string[] rolesController = _roles.Select(a => a.ToString()).ToArray();
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // role auth failed, redirect to login page
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            foreach (string rol in rolesController)
            {
                try
                {
                    if (filterContext.HttpContext.User.IsInRole(rol))
                    {
                        IsInRole = true;
                    }
                }
                catch
                {
                }
            }

        #if (DEBUG)
            IsInRole = true;
        #endif
            if (!IsInRole)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/ErrorAutorizacionView.cshtml"
                };
                return;
            }


            base.OnAuthorization(filterContext);
        }

    }
}


