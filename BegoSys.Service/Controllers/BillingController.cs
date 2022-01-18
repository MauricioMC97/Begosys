using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using BegoSys.Common.Constantes;
using BegoSys.Core.Facturacion;
using BegoSys.TO;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BegoSys.Service.Controllers
{
    public class BillingController : ApiController
    {
        private IBillingRepository _repositorioFacturacion;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSys.Service.Controllers.BillingController"/>
        /// </summary>
        /// <param name="repository">Instancia del repositorio inyectada a través de spring.</param>
        public BillingController(IBillingRepository RepositorioFacturacion)
        {
            _repositorioFacturacion = RepositorioFacturacion;
        }


        //// GET: Billing
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route(ConstantesApi.GuardarPedidoUri)]
        public async Task<IHttpActionResult> GuardarPedido(FacturaTO DF)
        {
            //if (ModelState.IsValid)
            //{
                await _repositorioFacturacion.SalvarPedido(DF);
                return Ok();
            /*}
            else
            {
                return BadRequest();
            }*/
        }

        [HttpPost]
        [Route(ConstantesApi.AnularPedidoUri)]
        public async Task<IHttpActionResult> AnularPedido(long ipd, long idl, long idp)
        {
            //if (ModelState.IsValid)
            //{
            await _repositorioFacturacion.AnulaPedido(ipd, idl, idp);
            return Ok();
            /*}
            else
            {
                return BadRequest();
            }*/
        }

        [HttpPost]
        [Route(ConstantesApi.ImprimirPedidoUri)]
        public void ImprimirPedido(long NroF)
        {
            BegoSys.Core.Facturacion.BillingRepository CoreFacturacion = new BegoSys.Core.Facturacion.BillingRepository();
            CoreFacturacion.PrintReceiptForTransaction(CoreFacturacion.ConsultarFactura(NroF));
        }

    }
}