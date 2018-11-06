using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene los tipos de productos que se venden
    /// </summary>
    [Table("jbTipoProductos")]
    public partial class TipoProducto
    {
        /// <summary>
        /// Identificador del tipo de producto para usar en EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idTipoProducto")]
        public long IdTipoProducto { get; set; }
    }
}
