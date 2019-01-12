#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/05/19
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

#region Referencias
using Spring.Http.Client.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
#endregion

namespace IDEAM.SGDHM.Web.Util
{
    /// <summary>
    /// Interceptor del request que se ejecuta a través del RestTemplate de Spring
    /// </summary>
    public class TokenRequestInterceptor : IClientHttpRequestBeforeInterceptor
    {
        /// <summary>
        /// Método que se ejecuta antes de llevar a cabo una solicitud a través del RestTemplate
        /// </summary>
        /// <param name="request">request a ejecutar</param>
        public void BeforeExecute(IClientHttpRequestContext request)
        {
            if(HttpContext.Current != null && HttpContext.Current.User != null)
            {
                ClaimsPrincipal claimsPrincipal = (ClaimsPrincipal)HttpContext.Current.User;
                if (claimsPrincipal != null)
                {
                    Claim claim = claimsPrincipal.FindFirst("AccessToken");
                    if (claim != null)
                    {
                        string bearerToken = claim.Value;
                        request.Headers["Authorization"] = bearerToken;
                    }
                }
            }
        }
    }
}