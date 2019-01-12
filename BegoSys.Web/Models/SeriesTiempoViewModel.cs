#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/06/19
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
    public class SeriesTiempoViewModel
    {
        /// <summary>
        /// Parametro
        /// </summary>
        public string Parametro { get; set; }
        /// <summary>
        /// Etiqueta
        /// </summary>
        public string Etiqueta { get; set; }
        /// <summary>
        ///Descripcion
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// EjeY1
        /// </summary>
        public string EjeY1 { get; set; }
        /// <summary>
        /// EjeY2
        /// </summary>
        public string EjeY2 { get; set; }
        /// <summary>
        /// TipoLinea
        /// </summary>
        public string TipoLinea { get; set; }
        /// <summary>
        /// TipoBarra
        /// </summary>
        public string TipoBarra { get; set; }
        /// <summary>
        /// TipoLinea
        /// </summary>
        public string TipoSerie { get; set; }
    }
}