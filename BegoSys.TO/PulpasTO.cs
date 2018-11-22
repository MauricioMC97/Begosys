using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class PulpasTO
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

        ///<summary>
        ///Id persona que está elaborando la pulpa
        /// </summary>
        public long IdPersona { get; set; }

        /// <summary>
        /// Hora en la que inicio la preparación de la pulpa
        /// </summary>
        public DateTime HoraInicio { get; set; }

    }
}
