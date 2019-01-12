#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/05/19
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

using BegoSys.Common.Auxiliares;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Common.Atributos
{

    /// <summary>
    /// Atributo personalizado para asociarle descripciones a las enumeraciones o TOs, las
    /// cuales se traducen desde el archivo de mensajes.
    /// </summary>
    public class BegoSysDescripcionAttribute : DescriptionAttribute
    {
        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSys.Common.Atributos.SGDHMDescripcionAttribute"/>
        /// </summary>
        /// <param name="key">Identificador del mensaje a traducir</param>
        public BegoSysDescripcionAttribute(string key) :
            base(AuxiliarBegoSys.TraducirMensaje(key))
        {

        }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSys.Common.Atributos.SGDHMDescripcionAttribute"/>
        /// </summary>
        /// <param name="key">Identificador del mensaje a traducir</param>
        /// <param name="parameters">Parámetros a reemplazar en el mensaje</param>
        public BegoSysDescripcionAttribute(String messageKey, params object[] parameters)
            : base(AuxiliarBegoSys.TraducirMensaje(messageKey, parameters))
        {

        }
    }
}