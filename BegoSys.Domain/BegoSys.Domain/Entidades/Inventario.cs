using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("JBINVENTARIOS")]
    public partial class Inventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDREGISTRO")]
        public long IdRegistro { get; set; }

        [Column("IDINGREDIENTE")]
        public long? IdIngrediente { get; set; }

        [Column("IDENVASE")]
        public long? IdEnvase { get; set; }

        [Column("IDINSUMO")]
        public long? IdInsumo { get; set; }

        [Column("CANTIDADACTUAL")]
        public long CantidadActual { get; set; }

        [Column("CANTIDADMINIMA")]
        public long CantidadMinima { get; set; }

        [Column("UNIDADESACTUALES")]
        public long? UnidadesActuales { get; set; }
        
        [Column("UNIDADESMINIMAS")]
        public long? UnidadesMinimas { get; set; }
        
        [Column("COSTOPROMDIAACTUAL")]
        public double CostoPromDiaActual { get; set; }
        
        [Column("COSTOPROMEDIAUNIDAD")]
        public double CostoPromDiaUnidad { get; set; }

        [Column("IDLOCAL")]
        public long IdLocal { get; set; }
    }
}
