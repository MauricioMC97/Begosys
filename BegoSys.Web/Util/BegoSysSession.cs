#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/05/19
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

using BegoSys.Common.Auxiliares;
using BegoSys.Web.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BegoSys.Web.Util
{
    /// <summary>
    /// Clase personalizada para el manejo de sesión en la aplicación MVC
    /// </summary>
    public class BegoSysSession : AuxiliarSesion
    {
        public override string Init(object obj)
        {
            if (Resources == null)
            {
                Resources = Messages.ResourceManager.GetResourceSet(
                    CultureInfo.CurrentUICulture, true, true);
            }

            var usuario = obj as UsuarioViewModel;

            if (usuario != null)
            {
                UserId = usuario.Id;
                UserName = usuario.Nombre;
                ProfileElements = usuario.ElementosPerfil;
                Menu = null;

                return usuario.Token.access_token;
            }
            else
            {
                //TODO: Refresh (Extender sesión)
            }

            return null;
        }
    }
}