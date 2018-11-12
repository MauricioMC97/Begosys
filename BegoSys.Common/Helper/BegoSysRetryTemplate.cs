#region Copyright
/*
 * Created by:      Mauricio Medina
 * Created date:    2018/11/09
 * Modified by:     Mauricio Medina
 * Modified date:   2018/11/09
 * Company:         Bego Inversiones S.A.S
*/
#endregion
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Common.Helper
{
    /// <summary>
    /// Delegado que encapsula el método a reintentar
    /// </summary>
    /// <returns></returns>
    public delegate object BegoSysExecuteMethod();

    /// <summary>
    /// Plantilla que gestiona el esquema de reintentos
    /// </summary>
    public class BegoSysRetryTemplate
    {
        #region Propiedades
        /// <summary>
        /// Cantidad máxima de reintentos
        /// </summary>
        public int MaxRetry { get; set; }
        /// <summary>
        /// Cantidad de tiempo para el próximo reintento
        /// </summary>
        public TimeSpan Delay { get; set; }
        #endregion       

        #region Métodos
        /// <summary>
        /// Método que permite hacer ejecuciones de acciones con lógica de reintentos
        /// </summary>
        /// <param name="action">Acción a ejecutar</param>
        /// <returns>Resultado de la ejecución</returns>
        public object ExecuteWithRetry(BegoSysExecuteMethod action)
        {
            object result = null;

            if (action != null)
            {
                int currentRetry = 0;

                do
                {
                    try
                    {
                        result = Task.Run(() => action()).Result;
                        Task a = (Task)result;
                        if (a.Exception != null)
                        {
                            throw new Exception(a.Exception.InnerException != null ? a.Exception.InnerException.Message : a.Exception.Message);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        System.Threading.Thread.Sleep(Delay);
                        currentRetry++;

                        if (currentRetry == MaxRetry)
                            throw ex;
                    }
                } while (currentRetry < MaxRetry);
            }

            return result;
        }
        #endregion
    }
}

