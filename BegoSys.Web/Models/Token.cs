#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/06/19
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

#region Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
#endregion

namespace BegoSys.Web.Models
{
    /// <summary>
    /// Clase que representa un token de autenticación del backend
    /// </summary>
    public class Token
    {
        public string expires_in { get; set; }
        public string access_token { get; set; }
    }
}