using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    /// <summary>
    /// Tabla que contiene los costos asociados a la mano de obra para ser usada en calculos durante la operación
    /// </summary>
    [Table("JBCOSTOMANOOBRA")]
    public partial class CostoManoObra
    {
        /// <summary>
        /// Identificador del registro para ser usado por Entityframework
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDREGISTRO")]
        public long IdRegistro { get; set; }

        /// <summary>
        /// Identificador de la persona de la tabla jbPersonas
        /// </summary>
        [Required]
        [Column("IDPERSONA")]
        public long IdPersona { get; set; }

        /// <summary>
        /// Salario total incluye todos los costos auxilio de transporte y parafiscales
        /// </summary>
        [Required]
        [Column("SALARIO")]
        public long Salario { get; set; }

        /// <summary>
        /// Salario total dividido 30 dias
        /// </summary>
        [Required]
        [Column("SALARIODIA")]
        public long SalarioDia { get; set; }

        /// <summary>
        /// Salario total dividido 2592000 segundos de un mes
        /// </summary>
        [Required]
        [Column("SALARIOSEGUNDO")]
        public long SalarioSegundo { get; set; }
    }
}
