using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene las medidas para ser usadas en los envases recetas y costos
    /// </summary>
    [Table("jbMedidas")]
    public partial class Medida
    {
        /// <summary>
        /// Identificador de la medida para ser usado en el EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idMedida")]
        public long idMedida { get; set; }

        /// <summary>
        /// Descripción de la medida
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("nombreMedida")]
        public string nombreMedida { get; set; }
    }
}
