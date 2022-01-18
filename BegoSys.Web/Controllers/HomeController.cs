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
    public class HomeController : ControladorJsonNet
    {
        private RestTemplate _proxy;

        #region Constructores
        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        public HomeController()
        {
            _proxy = LocalizadorServicioBegoSystem<RestTemplate>.GetService();
        }

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="CapturasController"/>
        /// </summary>
        /// <param name="proxy">proxy rest inyectado a través del contenedor de instancias de spring</param>
        public HomeController(RestTemplate proxy)
        {
            _proxy = proxy;
        }
        #endregion



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "BegoSystem - JuiceBar.co ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}