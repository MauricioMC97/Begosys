using BegoSys.Core.Facturacion;
using BegoSys.Service.Controllers;
using BegoSys.TO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BegoSys.Service.Tests.Controllers
{

    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }


    [TestClass]
    public class BillingControllerTest
    {
        private BillingController controller = new BillingController(new BillingRepository());



        [TestMethod]
        public void IngresarFacturas()
        {
            List<FacturaTO> datos = new List<FacturaTO>();

            var factura = new FacturaTO
            {
                IdRegistro = 9426,
                IdPedidoDia = 1,
                NroResolucionDian = "18762012046532",
                NroFacturaDian = "7508362",
                Fecha = DateTime.Now,
                TipoDespacho = 1,
                Impuesto = 360,
                ValorTotal = 4500,
                EstadoFactura = "PENDIENTE",
                IdPersona = 3,
                IdLocal = 1

                /*IdRegistro: 1,
                IdPedidoDia: 1,
                NroResolucionDian: "X",
                NroFacturaDian: "X",
                Fecha: frmPedidos.FechaHora1.value, //d.getDate() + "/" + d.getMonth() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds(),
                TipoDespacho: frmPedidos.TipoDespacho1.value,
                Impuesto: frmPedidos.Impoconsumo.value,
                ValorTotal: frmPedidos.ValorTotal.value,
                EstadoFactura: 'PENDIENTE',
                IdPersona: 3,
                IdLocal: 1,
                DetallePedido: [{
                IdRegistro: 1,
                    IdRegFactura: 1,
                    IdProducto: frmPedidos.Producto1.value,
                    Cantidad: frmPedidos.Cantidad1.value,
                    ValorUnitario: frmPedidos.ValorUnitario1.value,
                    Subtotal: frmPedidos.Subtotal1.value,
                    Observaciones: frmPedidos.observaciones1.value
                }*/
        };
            datos.Add(factura);


            //token         
            var clientToken = new RestClient("http://192.168.1.243/JBCService");
            //var clientToken = new RestClient("http://localhost:3445");
            //var clientToken = new RestClient("http://localhost/JBCService");


            var requestToken = new RestRequest("token", Method.POST);
            requestToken.AddParameter("grant_type", "password");
            requestToken.AddParameter("username", "apiuser_bego");
            requestToken.AddParameter("password", "bego2019*");

            // execute the request
            IRestResponse responseToken = clientToken.Execute(requestToken);

            Token obj = JsonConvert.DeserializeObject<Token>(responseToken.Content);

            var client = new RestClient("http://192.168.1.243/JBCService/api/Billing");

            var request = new RestRequest("GuardarPedido", Method.POST);

            request.AddJsonBody(datos);

            request.AddHeader("Authorization", obj.token_type + " " + obj.access_token);

            // execute the request
            IRestResponse response = client.ExecuteAsPost(request, "POST");

            var objEstadoRegistro = response.Content;


        }
    }
}
