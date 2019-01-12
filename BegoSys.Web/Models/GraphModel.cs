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
    public class GraphModel
    {
        /// <summary>
        /// id
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// valueAxis
        /// </summary>
        public string valueAxis { get; set; }
        /// <summary>
        /// lineColor
        /// </summary>
        public string lineColor { get; set; }
        /// <summary>
        /// bullet
        /// </summary>
        public string bullet { get; set; }
        /// <summary>
        /// bulletBorderThickness
        /// </summary>
        public Int32 bulletBorderThickness { get; set; }
        /// <summary>
        /// hideBulletsCount
        /// </summary>
        public Int32 hideBulletsCount { get; set; }
        /// <summary>
        /// title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// valueField
        /// </summary>
        public string valueField { get; set; }
        /// <summary>
        /// fillAlphas
        /// </summary>
        public Int32 fillAlphas { get; set; }
        /// <summary>
        /// type
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// lineThickness
        /// </summary>
        public Int32 lineThickness { get; set; }
        // <summary>
        /// bulletAlpha
        /// </summary>
        public Int32 bulletAlpha { get; set; }
        // <summary>
        /// bulletSize
        /// </summary>
        public Int32 bulletSize { get; set; }
        // <summary>
        /// bulletBorderAlpha
        /// </summary>
        public Int32 bulletBorderAlpha { get; set; }

        public string titleAxe { get; set; }
    }
}