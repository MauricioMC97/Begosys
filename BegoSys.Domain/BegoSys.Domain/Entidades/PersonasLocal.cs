using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    [Table("JBPERSONASLOCAL")]
    public partial class PersonasLocal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDREGISTRO")]
        public long idRegistro { get; set; }

        [Required]
        [Column("IDPERSONA")]
        public long idPersona { get; set; }

        [Required]
        [Column("IDLOCAL")]
        public long idLocal { get; set; }


        [Required]
        [Column("FECHAINICIO")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column("FECHAFIN")]
        public DateTime FechaFin{ get; set; }

        //[Required]
        //[Column("HORAENTRADA")]
        //public DateTime HoraEntrada { get; set; }

        //[Required]
        //[Column("HORASALIDA")]
        //public DateTime HoraSalida { get; set; }
    }
}
