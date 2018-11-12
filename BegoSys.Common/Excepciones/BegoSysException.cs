#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/11/09
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2018/11/09
// Empresa                      : BEGO INVERSIONES S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
#endregion

namespace BegoSys.Common.Excepciones
{
    /// <summary>
    /// Clase personalizada de excepciones para el proyecto
    /// </summary>
    public class BegoSysException : ApplicationException
    {
        #region Constructores

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSysException"/>
        /// </summary>
        /// <param name="messageKey">El mensaje de la excepción</param>
        public BegoSysException(string messageKey) : base(AuxiliarBegoSys.TraducirMensaje(messageKey)) { }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSysException"/>
        /// </summary>
        /// <param name="messageKey">El mensaje de la excepción</param>
        /// <param name="exception">Inner exception</param>
        public BegoSysException(string messageKey, Exception exception)
            : base(AuxiliarBegoSys.TraducirMensaje(messageKey), exception) { }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSysException"/>
        /// </summary>
        /// <param name="messageKey">El mensaje de la excepción</param>        
        /// <param name="parameters">Parámetros de sustitución para el mensaje</param>
        public BegoSysException(string messageKey, params object[] parameters)
            : base(AuxiliarBegoSys.TraducirMensaje(messageKey, parameters)) { }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSysException"/>
        /// </summary>
        /// <param name="messageKey">El mensaje de la excepción</param>
        /// <param name="exception">Inner exception</param>
        /// <param name="parameters">Parámetros de sustitución para el mensaje</param>
        public BegoSysException(string messageKey, Exception exception, params object[] parameters)
            : base(AuxiliarBegoSys.TraducirMensaje(messageKey, parameters), exception) { }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSysException"/>
        /// </summary>
        /// <param name="messageKey">El mensaje de la excepción</param>        
        /// <param name="traducir">Indica si traduce el mensaje o no</param>
        public BegoSysException(string messageKey, bool traducir)
            : base((traducir) ? AuxiliarBegoSys.TraducirMensaje(messageKey) : messageKey) { }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSysException"/>
        /// </summary>
        /// <param name="messageKey">El mensaje de la excepción</param>        
        /// <param name="traducir">Indica si traduce el mensaje o no</param>
        /// <param name="exception">Excepción actual</param>
        public BegoSysException(string messageKey, bool traducir, Exception exception)
            : base((traducir) ? AuxiliarBegoSys.TraducirMensaje(messageKey) : messageKey, exception) { }

        public BegoSysException()
        {
        }

        protected BegoSysException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}

