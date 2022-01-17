#region Derechos Reservados
// ===================================================
// Desarrollado Por             : robert.medina
// Fecha de Creación            : 2017/07/25
// Modificado Por               : robert.medina
// Fecha Modificación           : 2017/07/25
// Empresa                      : Bego Inversiones SAS
// ===================================================
#endregion

namespace BegoSys.Common.Auxiliares
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Representa un error el cual contiene un contrato que entiende el cliente.
    /// </summary>
    [DataContract]
    public class ErrorJsonGenerico
    {
        /// <summary>
        /// Estado de la operacion. Error.
        /// </summary>
        [DataMember(IsRequired = true, Name = "status")]
        public string Estado { get; } = "error";

        /// <summary>
        /// Descripción del error.
        /// </summary>
        [DataMember(IsRequired = true, Name = "message")]
        public string Mensaje { get; set; }

        /// <summary>
        /// Errores anidados.
        /// </summary>
        [DataMember(IsRequired = false, Name = "inner_exceptions")]
        public IList<string> ExcepcionesAnidadas { get; set; }

        /// <summary>
        /// Traza de la pila de ejecución.
        /// </summary>
        [DataMember(IsRequired = false, Name = "stack_trace")]
        public string TrazaPila { get; set; }

        /// <summary>
        /// Crea una instancia de la clase a partir de una excepción.
        /// </summary>
        /// <param name="ex">excepción a partir de la cual construir el error.</param>
        /// <returns>error con los datos de la excepción.</returns>
        public static ErrorJsonGenerico DeExcepcion(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            ErrorJsonGenerico result = new ErrorJsonGenerico()
            {
                Mensaje = ex.Message,
                ExcepcionesAnidadas = ObtenerExcepcionesAnidadas(ex),
                TrazaPila = ex.StackTrace
            };

            return result;
        }

        /// <summary>
        /// Obtiene una lista con todas las descripciones de las excepciones anidadas de una excepción determinada.
        /// </summary>
        /// <param name="ex">excepcion de la cual extraer las excepciones anidadas.</param>
        /// <returns>lista de descripciones de las exepciones.</returns>
        public static IList<string> ObtenerExcepcionesAnidadas(Exception ex)
        {
            Exception currentException = ex.InnerException;

            IList<string> exceptions = new List<string>();

            while (currentException != null)
            {
                exceptions.Add(currentException.Message);

                currentException = currentException.InnerException;
            }

            return exceptions;
        }
    }
}

