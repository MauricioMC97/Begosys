using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BegoSys.Domain.Entidades
{
    [Table("jbMonedas")]
    public partial class Moneda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("idMoneda")]
        public long IdMoneda { get; set; }

        [Required]
        [StringLength(3)]
        [Column("codigoMoneda")]
        public string CodigoMoneda { get; set; }

        [StringLength(3)]
        [Column("numMoneda")]
        public string NumMoneda { get; set; }


        [Column("DigitosDespuesDecimal")]
        public long DigitosDespuesDecimal { get; set; }

        [Required]
        [StringLength(200)]
        [Column("nombreMoneda")]
        public string NombreMoneda { get; set; }

        [Column("TasaCHoyPesos")]
        public long TasaCHoyPesos { get; set; }
    }
}

