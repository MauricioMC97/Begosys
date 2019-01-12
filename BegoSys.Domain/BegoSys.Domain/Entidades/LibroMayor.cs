using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("JBLIBROMAYOR")]
    public partial class LibroMayor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDREGISTRO")]
        public long IdRegistro { get; set; }

        [Column("IDCLASE")]
        public string IdClase { get; set; }

        [Column("IDGRUPO")]
        public string IdGrupo { get; set; }

        [Column("IDCUENTA")]
        public string IdCuenta { get; set; }

        [Column("IDSUBCUENTA")]
        public string IdSubCuenta { get; set; }

        [Column("FECHAHORA")]
        public DateTime FechaHora { get; set; }

        [Column("VALORDEBITO")]
        public double ValorDebito { get; set; }


        [Column("NRODOCPERSONADB")]
        public string NroDocPersonaDB { get; set; }

        [Column("VALORCREDITO")]
        public double ValorCredito { get; set; }

        [Column("NRODOCPERSONACR")]
        public string NroDocPersonaCR { get; set; }
        
        [Column("OBSERVACIONES")]
        public string Observaciones { get; set; }
        
    }
}
