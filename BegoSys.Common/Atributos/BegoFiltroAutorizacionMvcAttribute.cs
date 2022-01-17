using System.Linq;
using System.Web.Mvc;

namespace BegoSys.Common.Atributos
{
    public class BegoFiltroAutorizacionMvcAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    return;
                }
                else
                {
                    var atributosPersonalizados = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeAttribute), false);

                    if (atributosPersonalizados.Any())
                    {
                        return;
                    }
                    else
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                        filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        filterContext.HttpContext.Response.End();
                    }
                }
            }
        }
    }
}
