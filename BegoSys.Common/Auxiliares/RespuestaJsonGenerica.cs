#region Derechos Reservados
// ===================================================
// Desarrollado Por             : robert.medina
// Fecha de Creación            : 2017/08/10
// Modificado Por               : robert.medina
// Fecha Modificación           : 2017/08/10
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

namespace BegoSys.Common.Auxiliares
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Representa una respuesta de una operación la cual contiene un contrato que entiende el cliente.
    /// </summary>
    /// <typeparam name="T">tipo de dato del dato devuelto hacia el cliente.</typeparam>
    [DataContract]
    public class RespuestaJsonGenerica<T>
    {
        /// <summary>
        /// Estado de la operación. OK.
        /// </summary>
        [DataMember(IsRequired = true, Name = "status")]
        public string Estado { get; } = "ok";

        /// <summary>
        /// Dato a devolver al cliente.
        /// </summary>
        [DataMember(IsRequired = false, Name = "result")]
        public T Resultado { get; set; }
    }
}

