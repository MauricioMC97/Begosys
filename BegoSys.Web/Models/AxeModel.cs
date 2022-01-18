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
    public class AxehModel
    {
        /// <summary>
        /// valueAxis
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// lineColor
        /// </summary>
        public string axisColor { get; set; }
        /// <summary>
        /// axisThickness
        /// </summary>
        public Int32 axisThickness { get; set; }
        /// <summary>
        /// axisAlpha
        /// </summary>
        public Int32 axisAlpha { get; set; }
        /// <summary>
        /// position
        /// </summary>
        public string position { get; set; }           
        public Int32 offset { get; set; }
        public string title { get; set; }
    }
}