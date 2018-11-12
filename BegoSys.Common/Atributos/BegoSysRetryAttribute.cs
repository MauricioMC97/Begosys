#region Copyright
/*
 * Created by:      Mauricio Medina
 * Created date:    2018/11/09
 * Modified by:     Mauricio Medina
 * Modified date:   2018/11/09
 * Company:         Bego Inversiones S.A.S
*/
#endregion

using BegoSys.Common.Auxiliares;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Common.Atributos
{
    /// <summary>
    /// Atributo personalizado para reintentar un método
    /// </summary>
    public class BegoSysRetryAttribute : Attribute
    {
        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public BegoSysRetryAttribute()
        {
            string configMaxRetry = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("MaxRetry", false);
            string configDelay = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("DelayRetry", false);

            int maxRetry;

            if (!int.TryParse(configMaxRetry, out maxRetry))
            {
                maxRetry = 3;
            }

            MaxRetry = maxRetry;

            Delay = (string.IsNullOrEmpty(configDelay)) ? "00:00:05" : configDelay.ToString();
        }

        /// <summary>
        /// Constructor con parametros iniciales
        /// </summary>
        /// <param name="maxRetry">Cantidad máxima de reintentos</param>
        /// <param name="delay">Cantidad de tiempo para el próximo reintento</param>
        public BegoSysRetryAttribute(int maxRetry, string delay)
        {
            MaxRetry = maxRetry;
            Delay = delay;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Cantidad máxima de reintentos
        /// </summary>
        public int? MaxRetry { get; set; }
        /// <summary>
        /// Cantidad de tiempo para el próximo reintento
        /// </summary>
        public string Delay { get; set; }
        #endregion
    }
}

