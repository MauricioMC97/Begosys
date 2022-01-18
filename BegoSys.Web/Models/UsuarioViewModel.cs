#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/06/19
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoSys.Web.Models
{
    /// <summary>
    /// Representa un usuario actual del sistema
    /// </summary>
    public class UsuarioViewModel
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Rol actual del usuario
        /// </summary>
        public string Rol { get; set; }
        /// <summary>
        /// Email del usuario
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Obtiene los elementos del perfil del usuario
        /// </summary>
        public ICollection<string> ElementosPerfil { get; set; }
        /// <summary>
        /// Obtiene o establece el token generado en la autenticación con la API
        /// </summary>
        public Token Token { get; set; }
    }
}