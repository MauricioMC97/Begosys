using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene los nombres de los ingredientes de las recetas de la empresa
    /// </summary>
    [Table("jbIngredientes")]
    public partial class Ingrediente
    {
        /// <summary>
        /// Identificador del ingrediente para ser usado en EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idIngrediente")]
        public long idIngrediente { get; set; }

        /// <summary>
        /// Nombre del ingrediente
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("nombreIngrediente")]
        public string nombreIngrediente { get; set; }

        /// <summary>
        /// Ingrediente a partir del cual se llega a este ingrediente
        /// </summary>
        [Column("IngredienteOrigen")]
        public long IngredienteOrigen { get; set; }

        /// <summary>
        /// Tipo de ingrediente viene de la tabla jbTipoIngredientes
        /// </summary>
        [Required]
        [Column("idTipoIngrediente")]
        public long idTipoIngrediente { get; set; }

        #region Propiedades de Navegación
        [ForeignKey("idTipoIngrediente")]
        public virtual TipoIngrediente TipoIngredientes { get; set; }
        #endregion
    }
}
