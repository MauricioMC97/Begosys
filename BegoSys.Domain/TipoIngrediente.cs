using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    [Table("jbTipoIngredientes")]
    public partial class TipoIngrediente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idTipoIngrediente")]
        public long IdTipoIngrediente { get; set; }

        [Required]
        [StringLength(200)]
        [Column("nombreTipoIngrediente")]
        public string nombreTipoIngrediente { get; set; }

    }
}
