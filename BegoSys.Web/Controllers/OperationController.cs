using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Rest.Client;
using BegoSys.Common.Auxiliares;
using BegoSys.Common.ProveedoresDependencias;

namespace BegoSys.Web.Controllers
{
    public class OperationController : ControladorJsonNet
    {
        private RestTemplate _proxy;

        #region Constructores
        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        public OperationController()
        {
            _proxy = LocalizadorServicioBegoSystem<RestTemplate>.GetService();
        }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        /// <param name="proxy">proxy rest inyectado a través del contenedor de instancias de spring</param>
        public OperationController(RestTemplate proxy)
        {
            _proxy = proxy;
        }
        #endregion



        // GET: Operation
        public ActionResult Index()
        {
            return View();
        }
    }
}