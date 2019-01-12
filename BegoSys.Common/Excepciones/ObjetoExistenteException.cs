#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio.medina
// Fecha de Creación            : 2018/12/17
// Modificado Por               : robert.medina
// Fecha Modificación           : 2018/12/17
// Empresa                      : bego inversiones S.A.S
// ===================================================
#endregion

namespace BegoSys.Common.Excepciones
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Excepción que se lanza cuando se intenta guardar un objeto ya existente en un repositorio específico.
    /// </summary>
    public class ObjetoExistenteException : BegoSysException
    {
        public ObjetoExistenteException()
        {
        }

        protected ObjetoExistenteException(string messageKey) : base(messageKey)
        {
        }

        protected ObjetoExistenteException(string messageKey, Exception exception) : base(messageKey, exception)
        {
        }

        protected ObjetoExistenteException(string messageKey, params object[] parameters) : base(messageKey, parameters)
        {
        }

        protected ObjetoExistenteException(string messageKey, bool traducir) : base(messageKey, traducir)
        {
        }

        protected ObjetoExistenteException(string messageKey, Exception exception, params object[] parameters) : base(messageKey, exception, parameters)
        {
        }

        protected ObjetoExistenteException(string messageKey, bool traducir, Exception exception) : base(messageKey, traducir, exception)
        {
        }

        protected ObjetoExistenteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
