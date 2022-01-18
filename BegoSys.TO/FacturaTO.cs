using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class FacturaTO
    {
        public long IdRegistro { get; set; }

        public long IdPedidoDia { get; set; }

        public string NroResolucionDian { get; set; }

        public string NroFacturaDian { get; set; }

        public DateTime Fecha { get; set; }

        public long TipoDespacho { get; set; }

        public double Impuesto { get; set; }

        public double ValorTotal { get; set; }

        public string EstadoFactura { get; set; }

        public long IdPersona { get; set; }

        public long IdLocal { get; set; }

        public List<DetalleFacturaTO> DetallePedido { get; set; }

        public string Direccion { get; set; }

        public string DocCliente { get; set; }

        public string Cliente { get; set; }

        public string Telefono { get; set; }

    }
}
