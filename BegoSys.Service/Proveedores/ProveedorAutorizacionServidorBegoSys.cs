#region Derechos Reservados
// ===================================================
// Desarrollado Por             : MAURICIO MEDINA
// Fecha de Creación            : 2018/12/18
// Modificado Por               : MAURICIO MEDINA
// Fecha Modificación           : 2018/12/18
// Empresa                      : BEGO INVERSIONES  S.A.S
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
    public class ProveedorAutorizacionServidorSgdhm : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //Se obtienen las credenciales por defecto para el consumo del servicio
            string defaultUserName = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("DefaultUserNameAPI", true);
            string defaultPassword = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("DefaultPasswordAPI", true);
            bool isValid = (defaultUserName.ToUpper() == context.UserName?.ToUpper() && defaultPassword.ToUpper() == context.Password?.ToUpper());

            if (!isValid)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
            return Task.FromResult<object>(null);
        }
    }
}