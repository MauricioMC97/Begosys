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
        //public Municipio()
        //{
        //    CodigoCorregimientoLocal = new HashSet<Local>();
        //    MunicipioLocal = new HashSet<Local>();
        //    DeptoLocal = new HashSet<Local>();
        //    PaisLocal = new HashSet<Local>();
        //    IdMunicipioLocal = new HashSet<Local>();
        //}
        /// <summary>
        /// Consecutivo identificador del municipio para usar con el EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IdMunicipio", Order = 5)]
        public long IdMunicipio { get; set; }

        /// <summary>
        /// Código del departamento de la DIAN
        /// </summary>
        [Key]
        [Required]
        [StringLength(2)]
        [Column("codigoDeptoDian", Order = 3)]
        public string CodigoDeptoDian { get; set; }

        /// <summary>
        /// Identificador del municipio de la Dian
        /// </summary>
        [Key]
        [Required]
        [StringLength(5)]
        [Column("codigoMunicipio", Order = 2)]
        public string CodigoMunicipio { get; set; }

        /// <summary>
        /// Nombre del municipio
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("nombreMunicipio")]
        public string NombreMunicipio { get; set; }

        /// <summary>
        /// Código del corregimiento
        /// </summary>
        [Key]
        [Required]
        [StringLength(8)]
        [Column("codigoCorregimiento", Order = 1)]
        public string CodigoCorregimiento { get; set; }

        /// <summary>
        /// Nombre del corregimiento
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("nombreCorregimiento")]
        public string NombreCorregimiento { get; set; }

        /// <summary>
        /// Identificador del País al que pertenece el municipio
        /// </summary>
        [Key]
        [Required]
        [Column("NroPais", Order = 4)]
        public long NroPais { get; set; }


        #region Propiedades de navegación

        //[InverseProperty("CorregimientoL")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Local> CodigoCorregimientoLocal { get; set; }

        //[InverseProperty("MunicipioL")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Local> MunicipioLocal { get; set; }

        //[InverseProperty("DeptoL")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Local> DeptoLocal { get; set; }

        //[InverseProperty("PaisL")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Local> PaisLocal { get; set; }

        //[InverseProperty("IdMunicipioL")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Local> IdMunicipioLocal { get; set; }

        /// <summary>
        /// Clave foranea con la tabla de departamentos
        /// </summary>
        [ForeignKey("CodigoDeptoDian")]
        public virtual Departamento Departamentos { get; set; }

        /// <summary>
        /// Clave foranea con la tabla de paises
        /// </summary>
        [ForeignKey("NroPais")]
        public virtual Pais Paises { get; set; }

        #endregion
    }
}
