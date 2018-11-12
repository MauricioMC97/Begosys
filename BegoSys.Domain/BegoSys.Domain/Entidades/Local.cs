using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene la información básica de los locales comerciales
    /// </summary>
    [Table("jbLocales")]
    public partial class Local
    {
        /// <summary>
        /// Construye una nueva instancia de la clase.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Local()
        {
            Municipios = new HashSet<Municipio>();
        }

        /// <summary>
        /// Identificador único del local para ser usado en EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idLocal")]
        public long idLocal { get; set; }

        /// <summary>
        /// Nombre del establecimiento de comercio
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("nombreLocal")]
        public string nombreLocal { get; set; }

        /// <summary>
        /// Dirección del establecimiento
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("direccionLocal")]
        public string direccionLocal { get; set; }

        /// <summary>
        /// Pais de ubicación del establecimiento
        /// </summary>
        [Required]
        [Column("NroPais")]
        public long NroPais { get; set; }

        /// <summary>
        /// Departamento o estado de ubicación del establecimiento
        /// </summary>
        [Required]
        [StringLength(2)]
        [Column("codigoDeptoDian")]
        public string codigoDeptoDian { get; set; }

        /// <summary>
        /// Municipio de ubicación del establecimiento
        /// </summary>
        [Required]
        [StringLength(5)]
        [Column("codigomunicipio")]
        public string codigomunicipio { get; set; }

        /// <summary>
        /// Corregimiento de ubicación del establecimiento
        /// </summary>
        [Required]
        [StringLength(8)]
        [Column("codigocorregimiento")]
        public string codigocorregimiento { get; set; }

        /// <summary>
        /// Moneda en la cual se manejan las transacciones
        /// </summary>
        [Required]
        [Column("idMoneda")]
        public long idMoneda { get; set; }

        /// <summary>
        /// Hora en la que comienza el primer proceso dentro del establecimiento así no esté abierto al público
        /// </summary>
        [Required]
        [Column("HoraAbre")]
        public DateTime HoraAbre { get; set; }

        /// <summary>
        /// Hora en la que termina el último proceso dentro del establecimiento así no esté abierto al público
        /// </summary>
        [Required]
        [Column("HoraCierra")]
        public DateTime HoraCierra { get; set; }

        /// <summary>
        /// Hora a la que comienza a hacerse el inventario de materias primas
        /// </summary>
        [Required]
        [Column("HoraIInventario")]
        public DateTime HoraIInventario { get; set; }

        /// <summary>
        /// Hora a la que termina de hacerse el inventario de materias primas
        /// </summary>
        [Required]
        [Column("HoraFInventario")]
        public DateTime HoraFInventario { get; set; }

        /// <summary>
        /// Hora en la que se comienza a picar las frutas para sacar la pulpa este proceso incluye el inventario de pulpas
        /// </summary>
        [Required]
        [Column("HoraIPulpa")]
        public DateTime HoraIPulpa { get; set; }

        /// <summary>
        /// Hora en la que se termina de picar las frutas para sacar la pulpa este proceso incluye el inventario de pulpas
        /// </summary>
        [Required]
        [Column("HoraFPulpa")]
        public DateTime HoraFPulpa { get; set; }

        /// <summary>
        /// Hora en la que se comienzan las ventas al público
        /// </summary>
        [Required]
        [Column("HoraIVentas")]
        public DateTime HoraIVentas { get; set; }

        /// <summary>
        /// Hora en la que se terminan las ventas al público
        /// </summary>
        [Required]
        [Column("HoraFVentas")]
        public DateTime HoraFVentas { get; set; }


    }
}
