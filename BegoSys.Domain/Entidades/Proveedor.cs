using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain.Entidades
{
    [Table("JBPROVEEDORES")]
    public partial class Proveedor
    {
        [Key]
        [Column("IDPROVEEDOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdProveedor { get; set; }

        [Column("NITPROVEEDOR")]
        public string NitProveedor { get; set; }

        [Column("NOMBREPROVEEDOR")]
        public string NombreProveedor { get; set; }

        [Column("DIRECCIONPROVEEDOR")]
        public string DireccionProveedor { get; set; }

        [Column("TELEFONOPROVEEDOR")]
        public string TelefonoProveedor { get; set; }

        [Column("NROPAIS")]
        public long NroPais { get; set; }

        [Column("CODIGODEPTODIAN")]
        public string CodigoDeptoDian { get; set; }

        [Column("CODIGOMUNICIPIO")]
        public string CodigoMunicipio { get; set; }

        [Column("CODIGOCORREGIMIENTO")]
        public string CodigoCorregimiento { get; set; }

        [Column("IDMUNICIPIO")]
        public long IdMunicipio { get; set; }
    }
}
