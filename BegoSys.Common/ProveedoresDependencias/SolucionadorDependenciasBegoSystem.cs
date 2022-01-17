#region Derechos Reservados
// ===================================================
// Desarrollado Por             : Mauricio Medina
// Fecha de Creación            : 2018/12/16
// Modificado Por               : Mauricio Medina
// Fecha Modificación           : 2017/12/16
// Empresa                      : BEGO INVERSIONES SAS
// ===================================================
#endregion

#region Referencias
using System;
using Spring.Context;
#endregion

namespace BegoSys.Common.ProveedoresDependencias
{
    public class SolucionadorDependenciaBegoSystem : AmbitoDependenciaBegoSystem, System.Web.Http.Dependencies.IDependencyResolver,
        System.Web.Mvc.IDependencyResolver
    {
        private IApplicationContext _context;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSys.Common.ProveedoresDependencias.SolucionadorDependenciaBegoSystem"/>
        /// </summary>
        /// <param name="context"></param>
        public SolucionadorDependenciaBegoSystem(IApplicationContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Starts a resolution scope
        /// </summary>
        /// <returns>The dependency scope.</returns>
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return new AmbitoDependenciaBegoSystem(_context);
        }
    }
}