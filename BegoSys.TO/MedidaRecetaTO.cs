using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class MedidaRecetaTO
    {
        public long IdProducto { get; set; }

        public long IdIngrediente { get; set; }

        //Cantidad del ingrediente
        public long Cantidad { get; set; }
    }
}
