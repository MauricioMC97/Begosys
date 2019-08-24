using BegoSys.Common.Auxiliares;
using BegoSys.Common.ProveedoresDependencias;
using Spring.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BegoSys.Web.Controllers
{
    public class InventoryController : ControladorJsonNet
    {
        private RestTemplate _proxy;

        #region Constructores
        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="InventoryController"/>
        /// </summary>
        public InventoryController()
        {
            _proxy = LocalizadorServicioBegoSystem<RestTemplate>.GetService();
        }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="InventoryController"/>
        /// </summary>
        /// <param name="proxy">proxy rest inyectado a través del contenedor de instancias de spring</param>
        public InventoryController(RestTemplate proxy)
        {
            _proxy = proxy;
        }
        #endregion


        //// GET: Inventory
        //public ActionResult Index()
        //{
        //    return View("Buys");
        //}

        public ActionResult Buy()
        {
            return View("Buys");
        }
    }
}