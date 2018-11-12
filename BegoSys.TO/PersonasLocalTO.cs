using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class PersonasLocalTO
    {
        //Id Local
        public long idLocalComercial { get; set; }

        //Id Persona
        public long idPersona { get; set; }

        //Documento Persona
        public string documento { get; set; }

        //Nombre Completo
        public string nombre { get; set; }
    }
}
