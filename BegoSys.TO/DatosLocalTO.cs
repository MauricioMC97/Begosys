using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class DatosLocalTO
    {
        //Identificador del Local comercial
        public long IdLocal { get; set; }

        //Nombre del local comercial
        public string NombreLocal { get; set; }

        //Moneda en la que trabaja el local comercial
        public long IdMoneda { get; set; }

        //Hora en la que abre el local comercial
        public DateTime HoraAbre { get; set; }
 
        //Hora en la que cierra el local comercial    
        public DateTime HoraCierra { get; set; }

        //Hora en la que comienza el inventario
        public DateTime HoraIInventario { get; set; }

        //Hora en la que finaliza el inventario
        public DateTime HoraFInventario { get; set; }

        //Hora en la que incia el picado de la pulpa
        public DateTime HoraIPulpa { get; set; }

        //Hora en la que finaliza el picado de la pulpa
        public DateTime HoraFPulpa { get; set; }

        //Hora en la que inician las ventas al publico
        public DateTime HoraIVentas { get; set; }

        //Hora en la que finalizan las ventas al publico
        public DateTime HoraFVentas { get; set; }

        //Lista de personas que trabajan en el local
        public List<PersonasLocalTO> ListaPersonas { get; set; }
    }
}
