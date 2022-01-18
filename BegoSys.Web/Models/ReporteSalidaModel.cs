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
    public class ReporteSalidaModel
    {
        public string IdEstacion { get; set; }
        public string NombreEstacion { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public short? Altitud { get; set; }
        public string IdParametro { get; set; }
        public string Etiqueta { get; set; }
        public string DescripcionSerie { get; set; }
        public string Frecuencia { get; set; }
        public string Fecha { get; set; }
        public string TipoValor { get; set; }
        public string Valor { get; set; }
    }
}