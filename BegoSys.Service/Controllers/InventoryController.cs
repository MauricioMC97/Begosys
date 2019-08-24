using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using BegoSys.Common.Constantes;
using BegoSys.Core.Inventario;
using BegoSys.TO;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BegoSys.Service.Controllers
{
    public class InventoryController : ApiController
    {
        private IInventoryRepository _repositorioInventario;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSys.Service.Controllers.OperationController"/>
        /// </summary>
        /// <param name="repository">Instancia del repositorio inyectada a través de spring.</param>
        public InventoryController(IInventoryRepository repositorioInventario)
        {
            _repositorioInventario = repositorioInventario;
        }


        [HttpPost]
        [ResponseType(typeof(void))]
        [Route(ConstantesApi.GuardarComprasUri)]
        public async Task<IHttpActionResult> GuardarCompras()
        {
            //if (ModelState.IsValid)
            //{Ese dise163]
            await _repositorioInventario.SalvarCompras();
            return Ok();
            /*}
            else
            {
                return BadRequest();
            }*/
        }

    }
}