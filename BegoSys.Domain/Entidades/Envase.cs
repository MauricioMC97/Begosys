using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    [Table("JBENVASES")]
    public partial class Envase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDENVASE")]
        public long IdEnvase { get; set; }

        [Required]
        [Column("NOMBREENVASE")]
        public string NombreEnvase { get; set; }

        [Column("CAPACIDAD")]
        public long Capacidad { get; set; }

        [Column("IDMEDIDA")]
        public long IdMedida { get; set; }
    }
}
