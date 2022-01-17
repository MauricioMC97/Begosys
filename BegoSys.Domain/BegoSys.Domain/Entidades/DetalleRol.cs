using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("JBDETALLEROLES")]
    public partial class DetalleRol
    {
        //Identificador del registro para ser usado en Entity Framework
        [Key]
        [Column("IDREGISTRO")]
        public long IdRegistro { get; set; }

        //Identificador del rol de la tabla jbRoles
        [Required]
        [Column("IDROL")]
        public long IdRol { get; set; }

        //Identificador de la persona en la tabla jbPersonas
        [Required]
        [Column("IDPERSONA")]
        public long IdPersona { get; set; }

        //Identificador del proceso en la tabla jbProcesos
        [Required]
        [Column("IDPROCESO")]
        public long IdProceso { get; set; }
        
    }
}
