using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene los nombres de los ingredientes de las recetas de la empresa
    /// </summary>
    [Table("JBINGREDIENTES")]
    public partial class Ingrediente
    {
        /// <summary>
        /// Identificador del ingrediente para ser usado en EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDINGREDIENTE")]
        public long IdIngrediente { get; set; }

        /// <summary>
        /// Nombre del ingrediente
        /// </summary>
        [Required]
        [StringLength(200)]
        [Column("NOMBREINGREDIENTE")]
        public string NombreIngrediente { get; set; }

        /// <summary>
        /// Ingrediente a partir del cual se llega a este ingrediente
        /// </summary>
        [Column("INGREDIENTEORIGEN")]
        public long IngredienteOrigen { get; set; }

        /// <summary>
        /// Tipo de ingrediente viene de la tabla jbTipoIngredientes
        /// </summary>
        [Required]
        [Column("IDTIPOINGREDIENTE")]
        public long IdTipoIngrediente { get; set; }

        /// <summary>
        /// Describe si se maneja inventario del producto (1) o no (0)
        /// </summary>
        [Required]
        [Column("INVENTARIO")]
        public int Inventario { get; set; }

        /// <summary>
        /// Describe la medida que se usará durante el inventario, puede ser diferente a las de las recetas
        /// pero al guardarlas en la tabla de inventario se deben convertir o a gramos o a mililitros hablando de pulpas o frutas
        /// </summary>
        [Required]
        [Column("IDMEDIDA")]
        public int IdMedida { get; set; }
    }
}
