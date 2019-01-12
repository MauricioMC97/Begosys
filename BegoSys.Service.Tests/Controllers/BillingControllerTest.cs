using BegoSys.Core.Facturacion;
using BegoSys.Service.Controllers;
using BegoSys.TO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BegoSys.Service.Tests.Controllers
{
    class BillingControllerTest
    {
        private BillingController controller = new BillingController(new BillingRepository());

        [TestMethod]
        public void IngresarFacturas(FacturaTO dfc)
        {
            //arrange
            //long idLocal = 1;
            //long idProceso = 1;

            //act
            //OkNegotiatedContentResult<System.Web.Http.IHttpActionResult> resultadoConvertido = controller.GuardarPedido(dfc) as OkNegotiatedContentResult<System.Web.Http.IHttpActionResult>;

            //assert
            //Assert.IsNotNull(resultadoConvertido);
            //Assert.IsNotNull(resultadoConvertido.Content);
        }
    }
}
