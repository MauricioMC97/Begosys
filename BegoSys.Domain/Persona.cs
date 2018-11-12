using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("jbPersonas")]
    public partial class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idPersona")]
        public long idPersona { get; set; }

        [Required]
        [StringLength(20)]
        [Column("docPersona")]
        public string docPersona { get; set; }



        [Required]
        [StringLength(100)]
        [Column("nombresPersona")]
        public string nombresPersona { get; set; }


        [StringLength(100)]
        [Column("apellidosPersona")]
        public string apellidosPersona { get; set; }


        [Required]
        [StringLength(100)]
        [Column("nombrecompleto")] 
        public string nombrecompleto { get; set; }

        [Required]
        [StringLength(100)]
        [Column("tipoPersona")]
        public string tipoPersona { get; set; }

        [StringLength(100)]
        [Column("gerencia")]
        public string gerencia { get; set; }

        [StringLength(100)]
        [Column("subgerencia")]
        public string subgerencia { get; set; }

        [StringLength(100)]
        [Column("telefono")]
        public string telefono { get; set; }

        [StringLength(100)]
        [Column("correo")]
        public string correo { get; set; }

        [Column("idProveedor")]
        public long idproveedor { get; set; }
    }
}
