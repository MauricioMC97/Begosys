using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene los tipos de productos que se venden
    /// </summary>
    [Table("JBTIPOPRODUCTOS")]
    public partial class TipoProducto
    {
        /// <summary>
        /// Identificador del tipo de producto para usar en EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDTIPOPRODUCTO")]
        public long IdTipoProducto { get; set; }

        [Required]
        [StringLength(200)]
        [Column("NOMBRETIPOPRODUCTO")]
        public string nombreTipoProducto { get; set; }

    }
}
