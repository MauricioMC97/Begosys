using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    [Table("JBRESOLUCIONESDIAN")]
    public partial class ResolucionDian
    {
        [Key]
        [Column("IDRESOLUCIONDIAN")]
        public long IdResolucionDian { get; set; }

        [Required]
        [Column("NRORESOLUCIONDIAN")]
        public string NroResolucionDian { get; set; }
 
        [Required]
        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column("DESDE")]
        public string Desde { get; set; }

        [Required]
        [Column("HASTA")]
        public string Hasta { get; set; }

        [Required]
        [Column("ACTUAL")]
        public string Actual { get; set; }

        [Required]
        [Column("ACTIVA")]
        public int Activa { get; set; }

    }
}
