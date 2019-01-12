#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio.medina
// Fecha de Creación            : 2018/12/08
// Modificado Por               : mauricio.medina
// Fecha Modificación           : 2018/12/08
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

#region Referencias
using System;
using System.Collections;
using System.Linq;
using Spring.Context;
using Spring.Context.Support;
using BegoSys.Common.Excepciones;
#endregion

namespace BegoSys.Common.ProveedoresDependencias
{
    /// <summary>
    /// Clase que provee métodos para localizar y retornar un servicio específico desde el 
    /// contenedor de instancias
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LocalizadorServicioBegoSystem<T>
    {
        /// <summary>
        /// Retorna la instancia del servicio llamado nombreServicio
        /// en el contexto
        /// </summary>
        /// <returns>Instancia del servicio solicitado</returns>
        public static T GetService()
        {
            var result = LocalizadorServicioBegoSystem.GetService(typeof(T));

            if (result != null)
                return (T)result;
            return default(T);
        }
    }

    /// <summary>
    /// Clase utilizada para obtener la referencia a los servicios disponibles
    /// </summary>
    public class LocalizadorServicioBegoSystem
    {
        /// <summary>
        /// Palabra clave para identificar un objeto en el contenedor como servicio
        /// </summary>
        private const string ServiceKey = "Service";

        /// <summary>
        /// Retorna la instancia del servicio acorde al tipo
        /// </summary>
        /// <param name="serviceType">Tipo a buscar</param>
        /// <returns>Instancia del servicio solicitado</returns>
        public static object GetService(Type serviceType)
        {
            return GetService(serviceType, ContextRegistry.GetContext(), true);
        }

        /// <summary>
        /// Retorna la instancia del servicio acorde al tipo
        /// </summary>
        /// <param name="serviceType">Tipo a buscar</param>
        /// <param name="context">Contexto a utilizar</param>
        /// <returns>Instancia del servicio solicitado</returns>
        public static object GetService(Type serviceType, IApplicationContext context)
        {
            return GetService(serviceType, context, true);
        }

        /// <summary>
        /// Retorna la instancia del servicio acorde al tipo
        /// </summary>
        /// <param name="serviceType">Tipo a buscar</param>
        /// <param name="context">Contexto a utilizar</param>
        /// <param name="throwException">Indica si debe arrojar excepción en caso de no encontrar el servicio</param>
        /// <returns>Instancia del servicio solicitado</returns>
        public static object GetService(Type serviceType, IApplicationContext context, bool throwException)
        {
            System.Collections.Generic.IDictionary<string,object> dictionary = context.GetObjectsOfType(serviceType,true, true);
            if (dictionary.Count == 1)
            {
                //retorna el primero de los objetos de ese tipo ya que no se da el caso una misma interface registrada dos veces
                IEnumerator enumerator = dictionary.Values.GetEnumerator();
                enumerator.MoveNext();
                return enumerator.Current;
            }

            if (dictionary.Count > 1)
            {
                //si hay mas de un servicio registrado con la misma interface se procede a buscar el objeto cuyo nombre
                //contenga la palabra indicada en la constante SERVICE_KEY
                foreach (var key in from key in dictionary.Keys let serviceName = key where serviceName.Contains(ServiceKey) select key)
                {
                    return dictionary[key];
                }
            }

            if (!throwException)
                return null;

            //no se ha encontrado un servicio en el contenedor
            throw new BegoSysException("SGDHMError_ServiceLocatorException", serviceType);
        }
    }
}

