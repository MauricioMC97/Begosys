using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Common.Constantes
{
    public static class ConstantesApi
    {
        #region Operacion
        public const string ConsultarDatosLocalUri = "api/Operation/ConsultarDatosLocal/{id}";
        #endregion

        #region Accounting
        public const string GuardarenLibroMayor = "api/Accounting/GuardarenLibroMayor/{subcuenta}{debito}{personadebito}{credito}{personacredito}{fecha}{observaciones}";
        #endregion

        #region Billing
        public const string GuardarPedidoUri = "api/Billing/GuardarPedido";
        public const string AnularPedidoUri = "api/Billing/AnularPedido";
        public const string ImprimirPedidoUri = "api/Admin/ImprimirPedido";
        #endregion

        #region Inventory
        public const string TrasladarIngredienteURI = "api/Inventory/TrasladarIngrediente";
        public const string RegistrarCompraURI = "api/Inventory/RegistrarCompra";
        #endregion
    }
}
