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
    public class GraficoChartModel
    {
        /// <summary>
        /// valores a mostrar
        /// </summary>
        public List<Dictionary<string, object>> valores { get; set; }
        /// <summary>
        /// lista de axes
        /// </summary>
        public List<AxehModel> axes { get; set; }
        /// <summary>
        /// Rol actual del usuario
        /// </summary>
        public List<GraphModel> graphs { get; set; }
        /// <summary>
        /// campoCategoria
        /// </summary>
        public string campoCategoria { get; set; }
        /// <summary>
        /// mostrarTabla
        /// </summary>
        public bool mostrarTabla { get; set; }
    }
}