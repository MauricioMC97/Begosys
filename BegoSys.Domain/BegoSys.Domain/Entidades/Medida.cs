using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene las medidas para ser usadas en los envases recetas y costos
    /// </summary>
    [Table("JBMEDIDAS")]
    public partial class Medida
    {
        /// <summary>
        /// Identificador de la medida para ser usado en el EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDMEDIDA")]
        public long idMedida { get; set; }

        /// <summary>
        /// Descripción de la medida
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("NOMBREMEDIDA")]
        public string nombreMedida { get; set; }
    }
}
