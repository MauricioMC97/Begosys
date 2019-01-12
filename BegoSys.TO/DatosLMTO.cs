using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    //Datos que van para el libro mayor
    public class DatosLMTO
    {
        public long IdRegistro { get; set; }

        public string IdClase { get; set; }

        public string IdGrupo { get; set; }

        public string IdCuenta { get; set; }
        
        public string IdSubCuenta { get; set; }

        public DateTime FechaHora { get; set; }

        public double ValorDebito { get; set; }

        public string NroDocPersonaDB { get; set; }

        public double ValorCredito { get; set; }

        public string NroDocPersonaCR { get; set; }

        public string Observaciones { get; set; }
    }
}
