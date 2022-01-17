#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio.medina
// Fecha de Creación            : 2018/12/17
// Modificado Por               : mauricio.medina
// Fecha Modificación           : 2018/12/17
// Empresa                      : Bego Inversiones S.A.S
// ===================================================
#endregion

namespace BegoSys.Common.Excepciones
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Excepción que se lanza cuando se intenta obtener un objeto el cual no existe en un repositorio de datos específico.
    /// </summary>
    public class ObjetoNoEncontradoException : BegoSysException
    {
        public ObjetoNoEncontradoException()
        {
        }

        protected ObjetoNoEncontradoException(string messageKey) : base(messageKey)
        {
        }

        protected ObjetoNoEncontradoException(string messageKey, Exception exception) : base(messageKey, exception)
        {
        }

        protected ObjetoNoEncontradoException(string messageKey, params object[] parameters) : base(messageKey, parameters)
        {
        }

        protected ObjetoNoEncontradoException(string messageKey, bool traducir) : base(messageKey, traducir)
        {
        }

        protected ObjetoNoEncontradoException(string messageKey, Exception exception, params object[] parameters) : base(messageKey, exception, parameters)
        {
        }

        protected ObjetoNoEncontradoException(string messageKey, bool traducir, Exception exception) : base(messageKey, traducir, exception)
        {
        }

        protected ObjetoNoEncontradoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
