using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("JBMEDIDASRECETAS")]
    public partial class MedidasReceta
    {
        [Key]
        [Column("IDMEDIDARECETA")]
        public long IdMedidaReceta { get; set; }

        [Column("IDPRODUCTO")]
        public long IdProducto { get; set; }

        [Column("IDINGREDIENTE")]
        public long IdIngrediente { get; set; }

        [Column("IDENVASE")]
        public long IdEnvase { get; set; }

        [Column("CANTIDAD")]
        public long Cantidad { get; set; }

        [Column("IDMEDIDA")]
        public long IdMedida { get; set; }
    }
}
