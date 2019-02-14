using BegoSys.Common.Auxiliares;
using BegoSys.Common.Constantes;
using BegoSys.Common.ProveedoresDependencias;
using BegoSys.TO;
using Resources;
using Spring.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BegoSys.Web.Controllers
{
    public class BillingController : ControladorJsonNet
    {

        private RestTemplate _proxy;

        #region Constructores
        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        public BillingController()
        {
            _proxy = LocalizadorServicioBegoSystem<RestTemplate>.GetService();
        }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        /// <param name="proxy">proxy rest inyectado a través del contenedor de instancias de spring</param>
        public BillingController(RestTemplate proxy)
        {
            _proxy = proxy;
        }
        #endregion


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Permite guardar un pedido nuevo
        /// </summary>
        /// <param name="DatosFactura">Datos de la factura y del detalle de la factura</param>
        /// <returns>Retorna OK si la operación es exitosa</returns>
        [HttpPost]
        public async Task<ActionResult> GuardarPedido(FacturaTO DatosFactura)
        {
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            //if (ModelState.IsValid)
            //{
            //El registro acá llega inconsistente pero se van a agregar las claves correspondientes
            return Ok(await _proxy.PostForMessageAsync(ConstantesApi.GuardarPedidoUri, DatosFactura));
            //}
            //else
            //{
            //    return Error(Messages.Comun_CamposRequeridos);
            //}
        }

        /// <summary>
        /// Permite anular un pedido en estado PENDIENTE ya los pedidos en estado ENTREGADO no se pueden anular
        /// </summary>
        /// <param name="IdPedidoDia">Número del Pedido día</param>
        /// <returns>Retorna OK si la operación es exitosa</returns>
        [HttpPost]
        public async Task<ActionResult> AnularPedido(long IdPedidoDia)
        {
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            //if (ModelState.IsValid)
            //{
            //El registro acá llega inconsistente pero se van a agregar las claves correspondientes
            await _proxy.PostForMessageAsync(ConstantesApi.AnularPedidoUri, IdPedidoDia);
            return Ok();
            //}
            //else
            //{
            //    return Error(Messages.Comun_CamposRequeridos);
            //}
        }


        [HttpPost]
        public JsonResult ImprimirPedido(FacturaTO DatosFactura)
        {
            return Json(new { status = "ok" });
        }
    }
}
