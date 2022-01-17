using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    [Table("JBTIPOINGREDIENTES")]
    public partial class TipoIngrediente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDTIPOINGREDIENTE")]
        public long IdTipoIngrediente { get; set; }

        [Required]
        [StringLength(200)]
        [Column("NOMBRETIPOINGREDIENTE")]
        public string nombreTipoIngrediente { get; set; }

    }
}
