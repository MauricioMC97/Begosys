#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio.medina
// Fecha de Creación            : 2018/12/11
// Modificado Por               : mauricio.medina
// Fecha Modificación           : 2018/12/11
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Common.Atributos
{
    /// <summary>
    /// Atributo personalizado para asociarle códigos alfanuméricos a las enumeraciones.
    /// </summary>
    public class BegoSysCodigoAttribute : Attribute
    {

        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// </summary>
        /// <param name="code">Código de la enumeración.</param>
        public BegoSysCodigoAttribute(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Código de la enumeración.
        /// </summary>
        public string Code { get; set; }
    }
}
