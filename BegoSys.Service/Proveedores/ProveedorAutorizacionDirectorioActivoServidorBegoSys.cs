#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/12/18
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2018/12/18
// Empresa                      : BEGO INVERSIONES S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Auxiliares;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
#endregion

namespace BegoSys.Service.Proveedores
{
    /// <summary>
    /// Proveedor de autorización personalizado para el proyecto SGDHM
    /// </summary>
    public class ProveedorAutorizacionDirectorioActivoServidorSgdhm : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string userName = context.UserName;
            string password = context.Password;

            //Get domain from the request
            int domainIndex = userName.IndexOf(@"\");
            string domain = string.Empty;
            string defaultPassword = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("DefaultPasswordAPI", true);

            if (domainIndex != -1)
            {
                domain = userName.Substring(0, domainIndex);
                userName = userName.Substring(domainIndex + 1);
            }
            else
            {
                domain = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("DefaultDomainAPI", true);
            }

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(principalContext, userName);

                if (user == null || !user.Enabled.Value || defaultPassword.ToUpper() != password.ToUpper())
                {
                    context.SetError("invalid_grant", "username or password invalid");
                    return Task.FromResult<object>(null);
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            }

            return Task.FromResult<object>(null);
        }
    }
}