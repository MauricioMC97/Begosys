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

using BegoSys.Web.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BegoSys.Web.Util
{
    /// <summary>
    /// Atributo personalizado que permite interceptar solicitudes a la API para ejecutar
    /// acciones previas o posteriores al llamado
    /// </summary>
    public class SGDHMRoles
    {
        public SGDHMRoles()
        {
        }

        public static ICollection<string> GetRoles()
        {            
            ICollection<string> _roles = new List<string>();
            string[] separators = { "," };

            string [] Roles = System.Configuration.ConfigurationManager.AppSettings["RolesSistema"].ToString().Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string rol in Roles)
            {
                try
                {
                    if (HttpContext.Current.User.IsInRole(rol))
                    {
                        _roles.Add(rol);
                    }
                }
                catch 
                {
                }
            }
            return _roles;
        }
    }
}


