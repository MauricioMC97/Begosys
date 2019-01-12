using BegoSys.Web.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoSys.Web.Context
{
    
    
    public class Contexto
    {
        
        #region Atributos

        /// <summary>
        /// Identificación del usuario en sesión.
        /// </summary>
        private const string ID_USUARIO = "idUsuario";

        /// <summary>
        /// Nombre del usuario autenticado.
        /// </summary>
        private const string NOMBRE_USUARIO = "nombreUsuario";

        /// <summary>
        /// Rol del usuario autenticado en el sistema.
        /// </summary>
        private const string ROL_USUARIO = "nombreRol";

        /// <summary>
        /// Establece si el usuario logueado es válido para ingresar al sistema.
        /// </summary>
        private const string USUARIO_VALIDO = "usuarioValido";

        /// <summary>
        /// Login del usuario autenticado.
        /// </summary>
        private const string USUARIO = "usuario";

        /// <summary>
        /// Cadena que contiene la lista de opciones de menú autorizadas para el usuario
        /// autenticado.
        /// </summary>
        private const string XML_MENU_OPCIONES = "xmlConfiguracionMenu";     

        
        #endregion

        #region Propiedades

        /// <summary>
        /// Obtiene o establece la identificación del usuario, la cual equivale a la
        /// identificación del empleado.
        /// </summary>
        public static string Usuario
        {
            get
            {
                var valor = string.Empty;
                InMemoryCache CacheService = new InMemoryCache();
                var datos = CacheService.GetOrSet(Constantes.ConstantesSession.Usuario, () => valor);
                if (string.IsNullOrEmpty(datos))
                {
                    return string.Empty;
                }

                return datos;
            }

            set
            {
                InMemoryCache CacheService = new InMemoryCache();
                var datos = CacheService.GetOrSet(Constantes.ConstantesSession.Usuario, () => value);
            }
        }


        /// <summary>
        /// Obtiene o establece el nombre del usuario autenticado (equivale a los nombres y
        /// apellidos).
        /// </summary>
        public static string NombreUsuario
        {
            get
            {
                var valor = string.Empty;
                InMemoryCache CacheService = new InMemoryCache();
                var datos = CacheService.GetOrSet(Constantes.ConstantesSession.NombreUsuario, () => valor);
                if (string.IsNullOrEmpty(datos))
                {
                    return string.Empty;
                }

                return datos;
            }

            set
            {
                InMemoryCache CacheService = new InMemoryCache();
                var datos = CacheService.GetOrSet(Constantes.ConstantesSession.NombreUsuario, () => value);
            }
        }

        ///// <summary>
        ///// Obtiene o establece el rol actual que tiene el usuario autenticado.
        ///// </summary>
        //public static string RolUsuario
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[ROL_USUARIO] == null)
        //        {
        //            return string.Empty;
        //        }

        //        return HttpContext.Current.Session[ROL_USUARIO].ToString();
        //    }

        //    set
        //    {
        //        HttpContext.Current.Session[ROL_USUARIO] = value;
        //    }
        //}       
        #endregion
    }
}