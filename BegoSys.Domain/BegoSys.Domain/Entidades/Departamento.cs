using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene los códigos de los departamentos de la DIAN e ISO3166
    /// </summary>
    [Table("jbDepartamentos")]
    public partial class Departamento
    {
        /// <summary>
        /// Identificador del departamento se usa como indice para EntityFramework
        /// </summary>
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IdDepartamento")]
        public long IdDepartamento { get; set; }

        /// <summary>
        /// Código del departamento de la DIAN
        /// </summary>
        [Key]
        [Required]
        [StringLength(2)]
        [Column("CodigoDian")]
        public string CodigoDian { get; set; }

        /// <summary>
        /// Código del departamento ISO3166
        /// </summary>
        [Required]
        [StringLength(6)]
        [Column("CodeISO")]
        public string CodeISO { get; set; }

        /// <summary>
        /// Nombre del departamento
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("NombreDepto")]
        public string NombreDepto { get; set; }

        /// <summary>
        /// Identificador del País al que pertenece el departamento
        /// </summary>
        [Required]
        [Column("IdPais")]
        public long IdPais { get; set; }


        #region Propiedades de navegación

        /// <summary>
        /// Clave foranea con la tabla de paises
        /// </summary>
        [ForeignKey("IdPais")]
        public virtual Pais Paises { get; set; }

        #endregion

    }
}
