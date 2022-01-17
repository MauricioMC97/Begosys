using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("JBPERSONAS")]
    public partial class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDPERSONA")]
        public long idPersona { get; set; }

        [Required]
        [StringLength(20)]
        [Column("DOCPERSONA")]
        public string docPersona { get; set; }



        [Required]
        [StringLength(100)]
        [Column("NOMBRESPERSONA")]
        public string nombresPersona { get; set; }


        [StringLength(100)]
        [Column("APELLIDOSPERSONA")]
        public string apellidosPersona { get; set; }


        [Required]
        [StringLength(100)]
        [Column("NOMBRECOMPLETO")] 
        public string nombrecompleto { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TIPOPERSONA")]
        public string tipoPersona { get; set; }

        [StringLength(100)]
        [Column("GERENCIA")]
        public string gerencia { get; set; }

        [StringLength(100)]
        [Column("SUBGERENCIA")]
        public string subgerencia { get; set; }

        [StringLength(100)]
        [Column("TELEFONO")]
        public string telefono { get; set; }

        [StringLength(100)]
        [Column("CORREO")]
        public string correo { get; set; }

        [Column("IDPROVEEDOR")]
        public long idproveedor { get; set; }
    }
}
