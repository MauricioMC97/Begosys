#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio medina
// Fecha de Creación            : 2018/12/19
// Modificado Por               : mauricio medina
// Fecha Modificación           : 2018/12/19
// Empresa                      : BEGO INVERSIONES S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Service.Proveedores;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
#endregion

[assembly: OwinStartup(typeof(BegoSys.Service.Startup))]
namespace BegoSys.Service
{
    /// <summary>
    /// Clase que inicializa la capa media de Owin
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Método principal de configuración de Owin
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }

        /// <summary>
        /// Configura la autenticación por OAuth
        /// </summary>
        /// <param name="app">Aplicación a configurar</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            var oauthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ProveedorAutorizacionServidorSgdhm(),
                RefreshTokenProvider = new ProviderRefreshTokenServidorSgdhm()
            };

            //var windowsOauthServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/windowstoken"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    Provider = new ProveedorAutorizacionDirectorioActivoServidorSgdhm()
            //};

            app.UseOAuthAuthorizationServer(oauthServerOptions);
            //app.UseOAuthAuthorizationServer(windowsOauthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}