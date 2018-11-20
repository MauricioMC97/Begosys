using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    /// <summary>
    /// Clase que contiene los elementos a los cuales se les realizará inventario, se seleccionan de la tabla jbIngredientes, en este momento se incluyen
    /// 1. Frutas
    /// 3. Insumos
    /// </summary>
    public class InventarioTO
    {
        /// <summary>
        /// Identificador del ingrediente
        /// </summary>
        public long IdIngrediente { get; set; }
        
        /// <summary>
        /// Nombre del ingrediente
        /// </summary>
        public string NombreIngrediente { get; set; }

        /// <summary>
        /// Descripción de la Medida del ingrediente
        /// </summary>
        public string NombreMedida { get; set; }


    }
}
