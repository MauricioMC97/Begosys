using BegoSys.TO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BegoSys.Core.Facturacion
{
    public interface IBillingRepository
    {
        Task SalvarPedido(FacturaTO DFac);

        List<PedidoTO> ConsultarListaPedidos(string sDoc, DatosLocalTO dLoc);

        Task AnulaPedido(long ipd, long idLocal, long idPersona);

        void PrintReceiptForTransaction(FacturaTO DFac);
    }
}