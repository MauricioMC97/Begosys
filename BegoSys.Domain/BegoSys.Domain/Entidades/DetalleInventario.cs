using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("JBDETALLEINVENTARIOS")]
    public partial class DetalleInventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDREGISTRO")]
        public long IdRegistroDetInv { get; set; }

        [Required]
        [Column("FECHAHORA")]
        public DateTime FechaHora { get; set; }

        [Required]
        [Column("TRANSACCION")]
        public string Transaccion { get; set; }

        [Required]
        [Column("CANTIDAD")]
        public double Cantidad { get; set; }

        [Column("UNIDADES")]
        public double? Unidades { get; set; }

        [Required]
        [Column("COSTOTOTAL")]
        public double CostoTotal { get; set; }

        [Column("COSTOUNIDAD")]
        public double? CostoUnidad { get; set; }

        [Required]
        [Column("IDMEDIDA")]
        public long IdMedida { get; set; }
        
        [Column("IDINGREDIENTE")]
        public long? IdIngrediente { get; set; }
        
        [Column("IDENVASE")]
        public long? IdEnvase { get; set; }
        
        [Column("IDINSUMO")]
        public long? IdInsumo { get; set; }

        [Required]
        [Column("IDLOCAL")]
        public long IdLocal { get; set; }

        [Column("IDPROVEEDOR")]
        public long? IdProveedor { get; set; }

        [Column("TIEMPOPRODUCCION")]
        public long? TiempoProduccion { get; set; }

        [Column("PORCDESPERDICIO")]
        public long? PorcDesperdicio { get; set; }

        [Column("PORCAPROVECHABLE")]
        public long? PorcAprovechable { get; set; }

        [Column("IDPERSONA")]
        public long IdPersona { get; set; }

    }
}
