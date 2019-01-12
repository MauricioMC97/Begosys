using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    //Contiene los datos de cada uno de los productos comprados en la factura
    [Table("JBDETALLEFACTURAS")]
    public partial class DetalleFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDREGISTRO")]
        public long IdRegistro { get; set; }

        [Column("IDREGFACTURA")]
        public long IdRegFactura { get; set; }

        [Column("IDPRODUCTO")]
        public long IdProducto { get; set; }

        [Column("CANTIDAD")]
        public long Cantidad { get; set; }

        [Column("VALORUNITARIO")]
        public double ValorUnitario { get; set; }

        [Column("SUBTOTAL")]
        public double Subtotal { get; set; }

        [Column("OBSERVACIONES")]
        public string Observaciones { get; set; }

    }
}
