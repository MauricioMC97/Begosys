#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2016/08/05
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2016/08/05
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

using BegoSys.Common.Constantes;

namespace BegoSys.Web.Util
{
    /// <summary>
    /// Helper para el manejo de algunos estilos en la aplicación.
    /// </summary>
    public static class StylesUtil
    {
        // MÉTODOS:
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Obtiene el nombre del ícono font-awesome asociado a un estado de carga.
        /// </summary>
        /// <param name="estado">Estado a evaluar.</param>
        /// <returns>Nombre del ícono font-awesome asociado a un estado de carga.
        /// </returns>
        public static string GetIconClass(EstadoCarga? estado)
        {
            switch (estado)
            {
                case EstadoCarga.EnProgreso:
                case EstadoCarga.Reintentando:
                    return "fa fa-cogs";
                case EstadoCarga.Cancelado:
                case EstadoCarga.Desconocido:
                    return "fa fa-exclamation-triangle";
                case EstadoCarga.OK:
                    return "fa fa-check";
                case EstadoCarga.Error:
                    return "fa fa-times";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el color del ícono asociado a un estado de carga.
        /// </summary>
        /// <param name="estado">Estado a evaluar.</param>
        /// <returns>Nombre del ícono asociado al estado de carga.</returns>
        public static string GetIconColor(EstadoCarga? estado)
        {
            switch (estado)
            {
                case EstadoCarga.EnProgreso:
                case EstadoCarga.Reintentando:
                    return "#1963AA";
                case EstadoCarga.Cancelado:
                case EstadoCarga.Desconocido:
                    return "#EEA236";
                case EstadoCarga.OK:
                    return "#69AA46";
                case EstadoCarga.Error:
                    return "#D15B47";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el color del ícono asociado a un estado de carga.
        /// </summary>
        /// <param name="estado">Estado a evaluar.</param>
        /// <returns>Nombre del ícono asociado al estado de carga.</returns>
        public static string GetWidgetColor(EstadoCarga? estado)
        {
            switch (estado)
            {
                case EstadoCarga.EnProgreso:
                case EstadoCarga.Reintentando:
                    return "blue";
                case EstadoCarga.Cancelado:
                case EstadoCarga.Desconocido:
                    return "orange";
                case EstadoCarga.OK:
                    return "green";
                case EstadoCarga.Error:
                    return "red";
                default:
                    return string.Empty;
            }
        }
    }
}