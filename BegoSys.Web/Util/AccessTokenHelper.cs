#region Derechos Reservados
// ===================================================
// Desarrollado Por             : robert.medina
// Fecha de Creación            : 2017/05/19
// Modificado Por               : robert.medina
// Fecha Modificación           : 2017/05/19
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

namespace BegoSys.Web.Util
{
    using BegoSys.Common.Auxiliares;
    using BegoSys.Common.Excepciones;
    using BegoSys.Web.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web;

    /// <summary>
    /// Clase de utilidad que permite obtener el token de acceso asociado a la petición.
    /// </summary>
    public class AccessTokenHelper
    {
        /// <summary>
        /// Obtiene el token de acceso asociado a la petición que ejecuta el método.
        /// </summary>
        /// <returns>token de acceso asociado a la petición.</returns>
        public static string GetAccessToken()
        {
            string accessToken = string.Empty;

            if (HttpContext.Current.Request.Cookies["AccessToken"] != null)
            {
                accessToken = HttpContext.Current.Request.Cookies["AccessToken"].Value;
            }
            else
            {
                accessToken = GetAccessTokenForWindowsUser();
            }

            return accessToken;
        }

        /// <summary>
        /// Allows to get an access token for the current windows user
        /// </summary>
        /// <returns>An access token from the WebAPI</returns>
        private static string GetAccessTokenForWindowsUser()
        {
            string tokenUri = string.Format("{0}/windowstoken", AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("APIRoot", true));//Uri to the windows token
            string userName = HttpContext.Current.User.Identity.Name;
            string password = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("PasswordAPI", true);
            string accessToken = string.Empty;

            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password)
                });

                HttpResponseMessage result = httpClient.PostAsync(tokenUri, content).Result;

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new BegoSysException("SGDHMError_AuthenticationFailed");
                }

                string resultContent = result.Content.ReadAsStringAsync().Result;

                var token = JsonConvert.DeserializeObject<Token>(resultContent);
                accessToken = token.access_token;
                var cookie = new HttpCookie("AccessToken", accessToken);
                //set the cookie's expiration date
                cookie.Expires = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in) - (3600));
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            return accessToken;
        }
    }
}