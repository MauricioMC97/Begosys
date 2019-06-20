using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class InventarioInsumosTo
    {
        /// <summary>
        /// Identificador del insumo
        /// </summary>
        public long idInsumo { get; set; }

        /// <summary>
        /// Nombre del insumo
        /// </summary>
        public string nombreInsumo { get; set; }

        /// <summary>
        /// Cantidad del elemento en el inventario
        /// </summary>
        public double Cantidad { get; set; }

        /// <summary>
        /// Costo total del insumo para la cantidad actual
        /// </summary>
        public double CostoTotal { get; set; }
    }
}
