using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class DetalleFacturaTO
    {
        public long IdRegistro { get; set; }

        public long IdRegFactura { get; set; }

        public long IdProducto { get; set; }

        public long Cantidad { get; set; }

        public double ValorUnitario { get; set; }

        public double Subtotal { get; set; }

        public string Observaciones { get; set; }

    }
}
