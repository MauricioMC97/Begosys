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
    /// 2. Envases
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

        /// <summary>
        /// Persona que está elaborando el inventario de este producto
        /// </summary>
        public long idPersona { get; set; }

        /// <summary>
        /// Hora en la que comienza la elaboración del ingrediente procesado como las pulpas
        /// </summary>
        public DateTime HoraInicio { get; set; }

        /// <summary>
        /// Se guarda el valor de la medida que va en las recetas normalmente para descargarla del inventario
        /// </summary>
        public long MedidaReceta { get; set; }

        /// <summary>
        /// Guarda el identificador de la medida para enlazar los datos correctamente en el inventario
        /// </summary>
        public long IdMedida { get; set; }

        /// <summary>
        /// Cantidad del elemento en el inventario
        /// </summary>
        public double Cantidad { get; set; }

        /// <summary>
        /// Costo total del ingrediente para la cantidad actual
        /// </summary>
        public double CostoTotal { get; set; }
    }
}
