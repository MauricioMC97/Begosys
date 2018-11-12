using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene la información básica de los locales comerciales
    /// </summary>
    [Table("JBLOCALES")]
    public partial class Local
    {
        /// <summary>
        /// Identificador único del local para ser usado en EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDLOCAL")]
        public long IdLocal { get; set; }

        /// <summary>
        /// Nombre del establecimiento de comercio
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("NOMBRELOCAL")]
        public string NombreLocal { get; set; }

        /// <summary>
        /// Dirección del establecimiento
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("DIRECCIONLOCAL")]
        public string DireccionLocal { get; set; }

        /// <summary>
        /// Pais de ubicación del establecimiento
        /// </summary>
        [Required]
        [Column("NROPAIS")]
        //[ForeignKey("PaisL")]
        public long NroPais { get; set; }

        /// <summary>
        /// Departamento o estado de ubicación del establecimiento
        /// </summary>
        [Required]
        [StringLength(2)]
        [Column("CODIGODEPTODIAN")]
        //[ForeignKey("DeptoL")]
        public string CodigoDeptoDian { get; set; }

        /// <summary>
        /// Municipio de ubicación del establecimiento
        /// </summary>
        [Required]
        [StringLength(5)]
        [Column("CODIGOMUNICIPIO")]
        //[ForeignKey("MunicipioL")]
        public string CodigoMunicipio { get; set; }

        /// <summary>
        /// Corregimiento de ubicación del establecimiento
        /// </summary>
        [Required]
        [StringLength(8)]
        [Column("CODIGOCORREGIMIENTO")]
        //[ForeignKey("CorregimientoL")]
        public string CodigoCorregimiento { get; set; }

        /// <summary>
        /// Moneda en la cual se manejan las transacciones
        /// </summary>
        [Required]
        [Column("IDMONEDA")]
        public long IdMoneda { get; set; }

        /// <summary>
        /// Hora en la que comienza el primer proceso dentro del establecimiento así no esté abierto al público
        /// </summary>
        [Required]
        [Column("HORAABRE")]
        public DateTime HoraAbre { get; set; }

        /// <summary>
        /// Hora en la que termina el último proceso dentro del establecimiento así no esté abierto al público
        /// </summary>
        [Required]
        [Column("HORACIERRA")]
        public DateTime HoraCierra { get; set; }

        /// <summary>
        /// Hora a la que comienza a hacerse el inventario de materias primas
        /// </summary>
        [Required]
        [Column("HORAIINVENTARIO")]
        public DateTime HoraIInventario { get; set; }

        /// <summary>
        /// Hora a la que termina de hacerse el inventario de materias primas
        /// </summary>
        [Required]
        [Column("HORAFINVENTARIO")]
        public DateTime HoraFInventario { get; set; }

        /// <summary>
        /// Hora en la que se comienza a picar las frutas para sacar la pulpa este proceso incluye el inventario de pulpas
        /// </summary>
        [Required]
        [Column("HORAIPULPA")]
        public DateTime HoraIPulpa { get; set; }

        /// <summary>
        /// Hora en la que se termina de picar las frutas para sacar la pulpa este proceso incluye el inventario de pulpas
        /// </summary>
        [Required]
        [Column("HORAFPULPA")]
        public DateTime HoraFPulpa { get; set; }

        /// <summary>
        /// Hora en la que se comienzan las ventas al público
        /// </summary>
        [Required]
        [Column("HORAIVENTAS")]
        public DateTime HoraIVentas { get; set; }

        /// <summary>
        /// Hora en la que se terminan las ventas al público
        /// </summary>
        [Required]
        [Column("HORAFVENTAS")]
        public DateTime HoraFVentas { get; set; }

        /// <summary>
        /// Identificador del municipio en la tabla jbMunicipios se agrega solo para solucionar error entity framework
        /// </summary>
        [Required]
        [Column("IDMUNICIPIO")]
        //[ForeignKey("IdMunicipioL")]
        public long IdMunicipio { get; set; }

        //Propiedades de navegación

        [ForeignKey("CodigoCorregimiento, CodigoMunicipio, CodigoDeptoDian, NroPais, IdMunicipio")]
        public virtual Municipio Municipios { get; set; }

        //public virtual Municipio CorregimientoL { get; set; }

        //public virtual Municipio MunicipioL { get; set; }

        //public virtual Municipio DeptoL { get; set; }

        //public virtual Municipio PaisL { get; set; }

        //public virtual Municipio IdMunicipioL { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Moneda Monedas { get; set; }
    }
}
