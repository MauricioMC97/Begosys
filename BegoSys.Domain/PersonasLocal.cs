using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    [Table("jbPersonasLocal")]
    public partial class PersonasLocal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idRegistro")]
        public long idRegistro { get; set; }

        [Required]
        [Column("idPersona")]
        public long idPersona { get; set; }

        [Required]
        [Column("idLocal")]
        public long idLocal { get; set; }


        [Required]
        [Column("FechaInicio")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column("FechaFin")]
        public DateTime FechaFin{ get; set; }

        [Required]
        [Column("HoraEntrada")]
        public DateTime HoraEntrada { get; set; }

        [Required]
        [Column("HoraSalida")]
        public DateTime HoraSalida { get; set; }
    }
}
