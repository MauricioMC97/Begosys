#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/11/08
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2018/11/08
// Empresa                      : BEGO INVERSIONES SAS
// ===================================================
#endregion

#region Referencias
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BegoSys.Domain.Clases
{
    /// <summary>
    /// Encapsula las propiedades de un parametro de una funcion de base de datos. Para este contexto, función se refiere tanto a procedimientos almacenados como funciones.
    /// </summary>
    public class FunctionParameter
    {
        /// <summary>
        /// Construye una nueva instancia de la clase.
        /// </summary>
        public FunctionParameter()
        {

        }

        /// <summary>
        /// Construye una nueva instancia de la clase.
        /// </summary>
        /// <param name="parameterName">nombre del parametro.</param>
        /// <param name="oracleDbType">tipo del parametro.</param>
        /// <param name="direction">direccion del parametro.</param>
        /// <param name="value">valor del parametro.</param>
        public FunctionParameter(string parameterName, OracleDbType oracleDbType, ParameterDirection direction, object value)
        {
            ParameterName = parameterName;
            OracleDbType = oracleDbType;
            Direction = direction;
            Value = value;
        }

        /// <summary>
        /// Nombre del parámetro de la función.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Tipo del parámetro.
        /// </summary>
        public OracleDbType OracleDbType { get; set; }

        /// <summary>
        /// Dirección del parámetro.
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// Valor del parámetro.
        /// </summary>
        public object Value { get; set; }

    }
}

