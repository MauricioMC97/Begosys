using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla de municipios y corregimientos (usar corregimientos para mas exactitud)
    /// </summary>
    [Table("jbMunicipios")]
    public partial class Municipio
    {
        /// <summary>
        /// Consecutivo identificador del municipio para usar con el EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IdMunicipio")]
        public long IdMunicipio { get; set; }

        /// <summary>
        /// Código del departamento de la DIAN
        /// </summary>
        [Required]
        [StringLength(2)]
        [Column("codigoDeptoDian")]
        public string codigoDeptoDian { get; set; }

        /// <summary>
        /// Identificador del municipio de la Dian
        /// </summary>
        [Required]
        [StringLength(5)]
        [Column("codigoMunicipio")]
        public string codigoMunicipio { get; set; }

        /// <summary>
        /// Nombre del municipio
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("nombreMunicipio")]
        public string nombreMunicipio { get; set; }

        /// <summary>
        /// Código del corregimiento
        /// </summary>
        [Required]
        [StringLength(8)]
        [Column("codigoCorregimiento")]
        public string codigoCorregimiento { get; set; }

        /// <summary>
        /// Nombre del corregimiento
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("nombreCorregimiento")]
        public string nombreCorregimiento { get; set; }

        /// <summary>
        /// Identificador del País al que pertenece el municipio
        /// </summary>
        [Required]
        [Column("IdPais")]
        public long IdPais { get; set; }


        #region Propiedades de navegación

        /// <summary>
        /// Clave foranea con la tabla de departamentos
        /// </summary>
        [ForeignKey("codigoDeptoDian")]
        public virtual Departamento Departamentos { get; set; }

        /// <summary>
        /// Clave foranea con la tabla de paises
        /// </summary>
        [ForeignKey("IdPais")]
        public virtual Pais Paises { get; set; }

        #endregion
    }
}
