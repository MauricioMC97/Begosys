using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.TO
{
    public class PedidoTO
    {
        //Clase que contiene los pedidos de los clientes y la información para que se puedan atender

        //Número del pedido en el día
        public long IdPedidoDia { get; set; }

        //Nombre del producto
        public string NombreProd { get; set; }

        //Cantidad del producto
        public long Cantidad { get; set; }

        //Estado Pedido
        public string EstadoPedido { get; set; }
    }
}
