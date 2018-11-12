using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene los datos básicos de los paises ISO3166
    /// </summary>
    [Table("jbPaises")]
    public partial class Pais
    {
        /// <summary>
        /// Identificador único del país se usa como indice para el EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IdPais")]
        public long IdPais { get; set; }

        /// <summary>
        /// Código ISO 3166 del país
        /// </summary>
        [Required]
        [Column("NroPais")]
        public long NroPais { get; set; }

        /// <summary>
        /// Nombre del país
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("Nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Código del país ISO 3166-2 de dos caracteres
        /// </summary>
        [Required]
        [StringLength(2)]
        [Column("Alpha2Code")]
        public string Alpha2Code { get; set; }

        /// <summary>
        /// Código del país ISO 3166-3 de tres caracteres
        /// </summary>
        [Required]
        [StringLength(3)]
        [Column("Alpha3Code")]
        public string Alpha3Code { get; set; }

        /// <summary>
        /// Código de los paises que utiliza la Dian
        /// </summary>
        [Required]
        [Column("CodigoDian")]
        public string CodigoDian { get; set; }
    }
}

