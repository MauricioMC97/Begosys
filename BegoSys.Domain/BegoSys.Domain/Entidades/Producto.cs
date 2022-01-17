using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Contiene los datos básicos de los productos las acciones para encontrar los costos y las recetas correspondientes
    /// </summary>
    [Table("JBPRODUCTOS")]
    public partial class Producto
    {
        /// <summary>
        /// Contiene el identificador del producto para ser usado en EntityFramework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDPRODUCTO")]
        public long IdProducto { get; set; }

        [Required]
        [StringLength(200)]
        [Column("NOMBREPRODUCTO")]
        public string NombreProducto { get; set; }

        [Required]
        [Column("IDTIPOPRODUCTO")]
        public long IdTipoProducto { get; set; }

        [Required]
        [StringLength(2000)]
        [Column("RECETA")]
        public string Receta { get; set; }

        [Column("PRECIOCOSTOHOY")]
        public double PrecioCostoHoy { get; set; }

        [Required]
        [Column("PRECIOVENTAHOY")]
        public double PrecioVentaHoy { get; set; }

        [Column("MARGENHOY")]
        public double MargenHoy { get; set; }

        [Column("ACTIVO")]
        public long Activo { get; set; }

        #region Propiedades de Navegación
        ///<summary>
        ///Clave foranea a la tabla de tipos de producto
        ///</summary>

        #endregion

    }
}
