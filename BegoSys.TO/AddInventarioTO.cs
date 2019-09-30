using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    //Datos para adicionar al inventario
    public class AddInventarioTo
    {

        public long? IdIngrediente { get; set; }

        public long? IdEnvase { get; set; }

        public long? IdInsumo { get; set; }

        public double Cantidad { get; set; }

        public double Valor { get; set; }

        public double? NroUnidades { get; set; }

        public double? CostoUnidades { get; set; }

        public long IdMedida { get; set; }

        public long IdLocal { get; set; }

        public string NitProveedor { get; set; }

        public long IdPersona { get; set; }

    }
}
