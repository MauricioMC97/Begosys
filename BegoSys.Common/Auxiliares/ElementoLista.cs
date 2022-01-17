#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/11/08
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2018/11/08
// Empresa                      : BEGO INVERSIONES S.A.S
// ===================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BegoSys.Common.Auxiliares
{
    /// <summary>
    /// Permite representar un elemento de lista del tipo llave-valor
    /// </summary>
    [DataContract]
    public class ElementoLista
    {
        /// <summary>
        /// Constante para un elemento indefinido
        /// </summary>
        public const string ELEMENTO_INDEFINIDO = "--INDEFINIDO--";

        #region Constructores

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="IDEAM.SGDHM.Common.Auxiliares.ElementoLista"/>
        /// </summary>
        public ElementoLista()
        {
        }

        #endregion

        #region Propiedades
        /// <summary>
        /// Obtiene o establece el identificador del elemento
        /// </summary>
        [DataMember(Name = "clave")]
        public string Clave { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del padre del elemento.
        /// </summary>
        [DataMember(Name = "clave_padre")]
        public string ClavePadre { get; set; }

        /// <summary>
        /// Obtiene o establece el valor del elemento
        /// </summary>
        [DataMember(Name = "valor")]
        public string Valor { get; set; }

        /// <summary>
        /// Obtiene un GenericItem con sus propiedades establecidas como un String vacío.
        /// </summary>
        public static ElementoLista Vacio
        {
            get
            {
                return new ElementoLista()
                {
                    Clave = ELEMENTO_INDEFINIDO,
                    ClavePadre = null,
                    Valor = string.Empty
                };
            }
        }
        #endregion
    }
}
