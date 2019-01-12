#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio medina
// Fecha de Creación            : 2018-12-19
// Modificado Por               : mauricio medina
// Fecha Modificación           : 2018-12-19
// Empresa                      : BEGO INVERSIONES S.A.S
// ===================================================
#endregion

#region Referencias
using AutoMapper;
using AutoMapper.Configuration;
#endregion


namespace BegoSys.Core
{
    /// <summary>
    /// Clase estática que permite inicializar AutoMapper para toda la aplicación.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Inicializa AutoMapper para convertir automaticámente entidades de Entity Framework a Objetos de Transferencia.
        /// </summary>
        public static void RegistrarMapeosGlobales()
        {
            MapperConfigurationExpression configuracionAutoMapper = new MapperConfigurationExpression();

            configuracionAutoMapper.ShouldMapProperty = pi => pi.GetAccessors().Length > 0 ? !pi.GetAccessors()[0].IsVirtual : false;

            //EstablecerConfiguracionAutoMapper(ref configuracionAutoMapper);
            configuracionAutoMapper.AddProfiles(typeof(AutoMapperConfig).Assembly);

            Mapper.Initialize(configuracionAutoMapper);

            Mapper.AssertConfigurationIsValid();
        }
    }
}

