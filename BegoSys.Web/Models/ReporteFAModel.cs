#region Derechos Reservados
// ===================================================
// Desarrollado Por             : bernardo.bustamante
// Fecha de Creación            : 2017/10/25
// Modificado Por               : bernardo.bustamante
// Fecha Modificación           : 2017/10/25
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoSys.Web.Models
{
    /// <summary>
    /// Representa un usuario actual del sistema
    /// </summary>
    public class ReporteFAModel
    {
        public string Periodo { get; set; }
        public double valor { get; set; }
    }
}