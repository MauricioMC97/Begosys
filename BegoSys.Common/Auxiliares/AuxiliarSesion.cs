#region Derechos Reservados
// ===================================================
// Desarrollado Por             : juan.cuartas
// Fecha de Creación            : 2016/08/05
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/06/20
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion
using BegoSys.Common.ProveedoresDependencias;
using System.Collections.Generic;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BegoSys.Common.Auxiliares
{
    /// <summary>
    /// Utilidad para el manejo de las varibles de sesión.
    /// </summary>
    public abstract class AuxiliarSesion
    {
        // PROPIEDADES ALMACENADAS:
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Id del usuario autenticado.
        /// </summary>
        public string UserId
        {
            get { return Get<string>("UserId"); }
            set { Set("UserId", value); }
        }
        public string SessionId
        {
            get { return Get<string>("Id"); }
            set { Set("Id", value); }
        }

        /// <summary>
        /// Nombre del usuario autenticado.
        /// </summary>
        public string UserName
        {
            get { return Get<string>("UserName"); }
            set { Set("UserName", value); }
        }

        /// <summary>
        /// Lista de elementos sobre los que el usuario tiene autorización.
        /// </summary>
        public ICollection<string> ProfileElements
        {
            get { return Get<ICollection<string>>("ProfileElements"); }
            set { Set("ProfileElements", value); }
        }

        public ICollection<string> Roles
        {
            get { return Get<ICollection<string>>("Roles"); }
            set { Set("Roles", value); }
        }

        /// <summary>
        /// Menú HTML con las opciones del usuario autenticado.
        /// </summary>
        public string Menu
        {
            get { return Get<string>("Menu"); }
            set { Set("Menu", value); }
        }

        /// <summary>
        /// Archivo de recursos asociado al usuario autenticado.
        /// </summary>
        public ResourceSet Resources
        {
            get { return Get<ResourceSet>("Resources"); }
            set { Set("Resources", value); }
        }

        // PROPIEDADES COMPUTADAS:
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Indica si el usuario actual se encuentra autenticado.
        /// </summary>
        public bool IsAutenticated
        {
            get { return (UserId != null && SessionId != null); }
        }

        /// <summary>
        /// Idioma asociado al usuario actual.
        /// </summary>
        public string Language
        {
            get { return Thread.CurrentThread.CurrentCulture.Name; }
        }

        // PROPIEDADES ESTÁTICAS:
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Obtiene una referencia a la sesión actual.
        /// </summary>
        public static AuxiliarSesion Current
        {
            get
            {
                return LocalizadorServicioBegoSystem<AuxiliarSesion>.GetService();
            }
        }

        // MÉTODOS PÚBLICOS:
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Permite inicializar los valores de la sesión actual a partir del objeto
        /// recibido como argumento.
        /// </summary>
        /// <param name="obj">Objeto con la información de la sesión.</param>
        /// <returns>Token asignado a la sesión inicializada.</returns>
        public abstract string Init(object obj);

        /// <summary>
        /// Permite limpiar los valores de la sesión actual.
        /// </summary>
        public void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        // MÉTODOS PROTEGIDOS:
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Obtiene el valor almacenado en una variable de sessión.
        /// </summary>
        /// <typeparam name="T">Tipo al cual se convertirá el valor almacenado en la
        /// variable de sesión.</typeparam>
        /// <param name="name">Nombre de la variable de sesión.</param>
        /// <param name="defaultValue">Valor a retornar por defecto en caso que la
        /// variable sea nula.</param>
        /// <returns>Valor almacenado en la variable de sesión.</returns>
        protected T Get<T>(string name, T defaultValue = default(T))
        {
            var value = HttpContext.Current.Session[name];

            if (value != null)
            {
                return (T)value;
            }

            return defaultValue;
        }

        /// <summary>
        /// Permite almacenar el valor de una variable de sessión.
        /// </summary>
        /// <param name="name">Nombre de la variable de sessión.</param>
        /// <param name="value">Valor de la variable de sessión.</param>
        protected void Set(string name, object value)
        {
            HttpContext.Current.Session[name] = value;
        }
    }
}
