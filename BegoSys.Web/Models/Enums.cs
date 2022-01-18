#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/04/07
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/04/07
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BegoSys.Web.Enum
{
    /// <summary>
    /// Define el tipo de periodo para grid sinopticas
    /// </summary>
    public enum TipoDatosFrecuencia
    {
        // <summary>
        /// Hourly.
        /// </summary>
        Hourly = 0,
        // <summary>
        /// Daily.
        /// </summary>
        Daily = 1,
        /// <summary>
        /// Monthly.
        /// </summary>
        Monthly = 2,
        /// <summary>
        /// Decadal
        /// </summary>
        Decadal = 3,
        /// <summary>
        ///MultiAnual
        /// </summary>
        Multianual = 4
    }

    public enum TipoDatosReporteDiario
    {
        // <summary>
        /// Valor.
        /// </summary>
        Valor = 0,
        // <summary>
        /// Grado.
        /// </summary>
        Grado = 1,
        /// <summary>
        /// NivelAprobacion.
        /// </summary>
        NivelAprobacion = 2,
        /// <summary>
        /// Calificador
        /// </summary>
        Calificador = 3
    }  

}
