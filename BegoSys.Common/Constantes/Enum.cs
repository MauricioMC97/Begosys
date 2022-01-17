#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/11/08
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2018/11/08
// Empresa                      : BEGO INVERSIONES S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BegoSys.Common.Constantes
{
    #region Logging
    /// <summary>
    /// Categorías de log
    /// </summary>
    public enum LogCategory
    {
        [Description("ERROR")]
        Error,
        [Description("WARN")]
        Warning,
        [Description("INFO")]
        Information,
        [Description("DEBUG")]
        Debug,
        [Description("FATAL")]
        Fatal,
        /// <summary>
        /// Para los tipos de acción de traza del sistema.
        /// </summary>
        [Description("TRACE")]
        Trace
    }

    /// <summary>
    /// Severidad en el registro de log
    /// </summary>
    public enum LogSeverity
    {
        [Description("Critical")]
        Critical = 1,
        [Description("Error")]
        Error = 2,
        [Description("Warning")]
        Warning = 4,
        [Description("Information")]
        Information = 8,
        [Description("Verbose")]
        Verbose = 16,
        [Description("Start")]
        Start = 256,
        [Description("Stop")]
        Stop = 512,
        [Description("Suspend")]
        Suspend = 1024,
        [Description("Resume")]
        Resume = 2048,
        [Description("Transfer")]
        Transfer = 4096
    }
    /// <summary>
    /// Tipos de acciones para registro de logs.
    /// </summary>
    public enum TiposAccion
    {
        /// <summary>
        /// Para los tipos de acción de consulta.
        /// </summary>
        [Description("Consulta")]
        Consulta,
        /// <summary>
        /// Para los tipos de acción de creación.
        /// </summary>
        [Description("Creacion")]
        Creacion,
        /// <summary>
        /// Para los tipos de acción de eliminación.
        /// </summary>
        [Description("Eliminacion")]
        Eliminacion,
        /// <summary>
        /// Para los tipos de acción de modificación.
        /// </summary>
        [Description("Modificacion")]
        Modificacion,
        /// <summary>
        /// Para los tipos de acción de notificación.
        /// </summary>
        [Description("Notificacion")]
        Notificacion
    }
    #endregion

    #region Negocio

    public enum Role
    {
        // <summary>
        /// Rol asociado a las opciones de captura.
        /// </summary>
        R_DHIMEMP_CAPTURA,
        // <summary>
        /// Rol asociado a las opciones de consulta.
        /// </summary>
        R_DHIMEMP_CONSULTA,
        // <summary>
        /// Rol asociado a las opciones de administracion, y de consulta y captura.
        /// </summary>
        R_DHIMEMP_ADMIN,
        // <summary>
        /// Rol asociado a las opciones de catalogo de estaciones.
        /// </summary>
        R_DHIMEMP_ADMINCNE,
        // <summary>
        /// Rol asociado a las opciones de operacion red coordinador.
        /// </summary>
        R_DHIMEMP_COORDAOP,
        // <summary>
        /// Rol asociado a las opciones de operacion red planeacion.
        /// </summary>
        R_DHIMEMP_PLANOPER
    }
    #endregion

    /// <summary>
    /// Define los posibles estados de un proceso de carga.
    /// </summary>
    public enum EstadoCarga
    {
        [BegoSysDescripcionAttribute("EnumEstadoCarga_Error")]
        Error = 0,
        [BegoSysDescripcionAttribute("EnumEstadoCarga_OK")]
        OK = 1,
        [BegoSysDescripcionAttribute("EnumEstadoCarga_Reintentando")]
        Reintentando = 2,
        [BegoSysDescripcionAttribute("EnumEstadoCarga_Cancelado")]
        Cancelado = 3,
        [BegoSysDescripcionAttribute("EnumEstadoCarga_EnProgreso")]
        EnProgreso = 4,
        [BegoSysDescripcionAttribute("EnumEstadoCarga_Desconocido")]
        Desconocido = 5
    }

}

