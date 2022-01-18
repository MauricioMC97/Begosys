using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class TrasladoIngrTO
    {
        public long idIngrediente { get; set; }

        public long? iCantidadBolsas { get; set; }

        public double? iCantidadGramos { get; set; }

        public DatosLocalTO dDatosLocalOrigen { get; set; }

        public long idLocalDestino { get; set; }

        public DateTime dFEcha { get; set; }
    }
}
