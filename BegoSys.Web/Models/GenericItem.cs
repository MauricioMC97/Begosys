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
    /// Elemento generico para representación de valores constantes como un objeto Json
    /// </summary>
    public class GenericItem
    {
        /// <summary>
        /// Obtiene o establece el identificador del elemento
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Obtiene o establece el valor del elemento
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Obtiene o establece un valor indicando si el elemento
        /// se encuentra seleccionado
        /// </summary>
        public bool IsSelected { get; set; }
    }
}